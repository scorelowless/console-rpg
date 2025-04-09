namespace ProjOb;

public class KeyControlBuilder : IMapBuilder
{
    private bool _hasItems;
    private bool _hasUsables;
    private bool _hasWeapons;
    private bool _hasEnemies;
    private IKeyControl _keyControl = null!;

    public KeyControlBuilder()
    {
        Reset();
    }
    
    public void Reset()
    {
        _keyControl = new GuardKeyControl();
        _keyControl = new KeyControl(_keyControl, ConsoleKey.W, () => Game.GetPlayer.Move(Direction.Up));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.A, () => Game.GetPlayer.Move(Direction.Left));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.S, () => Game.GetPlayer.Move(Direction.Down));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.D, () => Game.GetPlayer.Move(Direction.Right));
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
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.E, Game.GetPlayer.PickUp, "Which item from the tile contents you want to pick up (number or letter)");
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.Q, Game.GetPlayer.ThrowAway, "Which item from the inventory you want to throw away (number or letter)");
        _keyControl = new AdvancedKeyControl(_keyControl, new ConsoleKeyInfo('Q', ConsoleKey.Q, true, false, false), Game.GetPlayer.DropEverythingNow);
        _hasItems = true;
    }

    public void AddWeapons(int n = 1)
    {
        if (_hasWeapons || n == 0) return;
        if(!_hasUsables) AddCurrencies();
        _keyControl = new KeyControl(_keyControl, ConsoleKey.T, () => Game.GetPlayer.Unequip());
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
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.R, Game.GetPlayer.Use, "Which item from the inventory you want to use/equip (number or letter)");
        _hasUsables = true;
    }

    public void AddEnemies(int n = 1)
    {
        if(_hasEnemies || n == 0) return;
        _keyControl = new KeyControl(_keyControl, ConsoleKey.X, () => { });
        _hasEnemies = true;
    }

    public object GetResult()
    {
        return _keyControl;
    }
}