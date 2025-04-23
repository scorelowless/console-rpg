namespace ProjOb;

public class KeyControlBuilder : IMapBuilder
{
    private bool _hasItems;
    private bool _hasUsables;
    private bool _hasWeapons;
    private bool _hasEnemies;
    private IKeyControl _keyControl = null!;
    private Player _player;

    public KeyControlBuilder(Player player)
    {
        Reset();
        _player = player;
    }
    
    public void Reset()
    {
        _keyControl = new GuardKeyControl();
        _keyControl = new KeyControl(_keyControl, ConsoleKey.W, () => _player.Move(Direction.Up));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.A, () => _player.Move(Direction.Left));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.S, () => _player.Move(Direction.Down));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.D, () => _player.Move(Direction.Right));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.Escape, Game.CurrentGame.Stop);
        _hasItems = false;
        _hasUsables = false;
        _hasWeapons = false;
        _hasEnemies = false;
    }

    public void Empty()
    {
    }

    public void Full()
    {
    }

    public void AddPaths(int n)
    {
    }

    public void AddDefaultPath()
    {
    }

    public void AddRooms(int n)
    {
    }

    public void AddMainRoom()
    {
    }

    public void AddItems(int n = 1)
    {
        if (_hasItems || n == 0) return;
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.E, _player.PickUp, "Which item from the tile contents you want to pick up (number or letter)");
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.Q, _player.ThrowAway, "Which item from the inventory you want to throw away (number or letter)");
        _keyControl = new AdvancedKeyControl(_keyControl, new ConsoleKeyInfo('Q', ConsoleKey.Q, true, false, false), _player.DropEverythingNow);
        _hasItems = true;
    }

    public void AddWeapons(int n = 1)
    {
        if (_hasWeapons || n == 0) return;
        if(!_hasUsables) AddCurrencies();
        _keyControl = new KeyControl(_keyControl, ConsoleKey.T, () => _player.Unequip());
        _hasWeapons = true;
    }

    public void AddEffectWeapons(int n = 1)
    {
        AddWeapons(n);
    }

    public void AddElixirs(int n = 1)
    {
        AddCurrencies(n);
    }

    public void AddCurrencies(int n = 1, int max = 1)
    {
        if (_hasUsables || n == 0) return;
        if (!_hasItems) AddItems();
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.R, _player.Use, "Which item from the inventory you want to use/equip (number or letter)");
        _hasUsables = true;
    }

    public void AddEnemies(int n = 1)
    {
        if(_hasEnemies || n == 0) return;
        string prompt = """
                        What kind of attack you want to do?
                        1. Normal attack
                        2. Hidden attack
                        3. Magic attack
                        """;
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.X, _player.Attack, prompt);
        _hasEnemies = true;
    }

    public object GetResult()
    {
        return _keyControl;
    }
}