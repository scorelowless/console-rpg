namespace ProjOb;

public class Game
{
    private bool _isRunning = true;
    private static Game? _instance;
    private Map _map;
    private Player _player;
    private Display _display;
    private KeyControl _keyControl;
    public event Action? GameTick; // TODO: how does this work? who can invoke this? what does invoking this event do?
    public static Game? GetGame => _instance;
    public static Player? GetPlayer => _instance?._player;
    private Game()
    {
        _instance = this;
        _map = MapBuilderDirector.GenerateBasicMap(new MapBuilder()) as Map ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        string instructions = MapBuilderDirector.GenerateBasicMap(new InstructionBuilder()) as string ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        _player = new Player(_map);
        _keyControl = MapBuilderDirector.GenerateBasicMap(new KeyControlBuilder()) as  KeyControl ?? throw new Exception("MapBuilderDirector.GenerateBasicMap returned null");
        _display = Display.GetInstance(_map, _player, instructions);
    }

    public static Game NewGame()
    {
        return _instance ?? new Game();
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