namespace ProjOb;

public class KeyControlBuilder : IMapBuilder
{
    private bool _hasItems;
    private bool _hasUsables;
    private bool _hasWeapons;
    private bool _hasEnemies;
    private IKeyControl _keyControl = null!;
    private int _playerIndex;
    private Display _display;

    public KeyControlBuilder(int playerIndex, Display display)
    {
        _playerIndex = playerIndex;
        Reset();
        _display = display;
    }
    
    public void Reset()
    {
        _keyControl = new GuardKeyControl();
        _keyControl = new KeyControl(_keyControl, ConsoleKey.W, ActionType.Move.Up(_playerIndex));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.A, ActionType.Move.Left(_playerIndex));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.S, ActionType.Move.Down(_playerIndex));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.D, ActionType.Move.Right(_playerIndex));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.Escape, new ActionType.Die(_playerIndex));
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
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.E, new ActionType.PickUp(_playerIndex), "Which item from the tile contents you want to pick up (number or letter)", _display);
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.Q, new ActionType.ThrowAway(_playerIndex), "Which item from the inventory you want to throw away (number or letter)", _display);
        _keyControl = new AdvancedKeyControl(_keyControl, new ConsoleKeyInfo('Q', ConsoleKey.Q, true, false, false), new ActionType.DropEverythingNow(_playerIndex));
        _hasItems = true;
    }

    public void AddWeapons(int n = 1)
    {
        if (_hasWeapons || n == 0) return;
        if(!_hasUsables) AddCurrencies();
        _keyControl = new KeyControl(_keyControl, ConsoleKey.T, new ActionType.Unequip(_playerIndex));
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
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.R, new ActionType.Use(_playerIndex), "Which item from the inventory you want to use/equip (number or letter)", _display);
        _hasUsables = true;
    }

    public void AddEnemies(int n = 1)
    {
        if(_hasEnemies || n == 0) return;
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.X, new ActionType.Attack(_playerIndex), """
            What kind of attack you want to do?
            1. Normal attack
            2. Hidden attack
            3. Magic attack
            """, _display);
        _hasEnemies = true;
    }

    public object GetResult()
    {
        return _keyControl;
    }
}