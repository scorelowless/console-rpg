namespace ProjOb;

public class Game
{
    private bool _isRunning = true;
    private readonly Player _player;
    private readonly KeyControl _keyControl;
    public static Game? CurrentGame { get; private set; }

    public static Player? GetPlayer => CurrentGame?._player;
    private Game()
    {
        CurrentGame = this;
        var map = MapBuilderDirector.GenerateBasicMap(new MapBuilder()) as Map ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        string instructions = MapBuilderDirector.GenerateBasicMap(new InstructionBuilder()) as string ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        _player = new Player(map);
        _keyControl = MapBuilderDirector.GenerateBasicMap(new KeyControlBuilder()) as  KeyControl ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        Display.GetInstance(map, _player, instructions);
    }

    public static Game NewGame()
    {
        return CurrentGame ?? new Game();
    }

    public void Start()
    {
        _isRunning = true;
        Thread keyListenerThread = new Thread(() =>
        {
            while (_isRunning)
            {
                if (!Console.KeyAvailable) continue;
                _keyControl.Check(Console.ReadKey(true).Key);
            }
        });
        keyListenerThread.Start();
    }

    public void Stop()
    {
        _isRunning = false;
    }
}