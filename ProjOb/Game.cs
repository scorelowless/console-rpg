namespace ProjOb;

public class Game
{
    private readonly IKeyControl _keyControl;
    private readonly Display _display;
    private static Game? _currentGameState;

    public static Game CurrentGame => _currentGameState ?? throw new Exception("CurrentGame invoked without invoking NewGame beforehand");
    
    private Game()
    {
        _currentGameState = this;
        var map = MapBuilderDirector.GenerateBasicMap(new MapBuilder()) as Map ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        string instructions = MapBuilderDirector.GenerateBasicMap(new InstructionBuilder()) as string ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        var player = new Player(map);
        _keyControl = MapBuilderDirector.GenerateBasicMap(new KeyControlBuilder(player)) as IKeyControl ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        _display = Display.GetInstance(map, player, instructions);
    }

    public static Game NewGame() // TODO: a lot to do with it when changing to multiplayer
    {
        Game _ = new Game();
        return CurrentGame;
    }

    public void Start()
    {
        bool isPrompting = false;
        while (true) 
        {
            if (!Console.KeyAvailable) continue;
            bool wasPrompting = isPrompting;
            IActionType result = _keyControl.Check(_display.ReadKey(), ref isPrompting);
            if (result.IsSenderDead)
            {
                break;
            }
            
            if (wasPrompting && !isPrompting)
            {
                _display.HideCursor();
            }
            if (result.IsPrompt)
            {
                _display.Prompt(result.Message);
            }
            else
            {
                if (result.WasSuccessful)
                {
                    _display.Update();
                }
                _display.Log(result.Message);
            }
            if (isPrompting && !wasPrompting) 
            {
                int lines = result.Message.Count(c => c == '\n') + 1;
                _display.ShowCursor(lines);
            }
        }
        _display.GameOver();
    }
}