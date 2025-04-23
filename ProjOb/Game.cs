namespace ProjOb;

public class Game
{
    private bool _isRunning = true;
    private readonly KeyControl _keyControl;
    private readonly Display _display;
    private static Game _currentGame = null!;

    public static Game CurrentGame => _currentGame ?? throw new Exception("CurrentGame invoked without invoking NewGame beforehand");
    
    private Game()
    {
        _currentGame = this;
        var map = MapBuilderDirector.GenerateBasicMap(new MapBuilder()) as Map ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        string instructions = MapBuilderDirector.GenerateBasicMap(new InstructionBuilder()) as string ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        var player = new Player(map);
        _keyControl = MapBuilderDirector.GenerateBasicMap(new KeyControlBuilder(player)) as KeyControl ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        _display = Display.GetInstance(map, player, instructions);
    }

    public static Game NewGame()
    {
        Game _ = new Game();
        return CurrentGame;
    }

    public void Start()
    {
        _isRunning = true;
        while (_isRunning)
        {
            if (!Console.KeyAvailable) continue;
            _keyControl.Check(Console.ReadKey(true));
        }
    }

    public void Stop()
    {
        _isRunning = false;
        _display.GameOver();
    }
}