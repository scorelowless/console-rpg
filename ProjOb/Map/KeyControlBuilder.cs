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
        _keyControl = new KeyControl(
            new KeyControl(
                new KeyControl(
                    new KeyControl(
                        new KeyControl(
                            new GuardKeyControl(), ConsoleKey.W, () => Game.GetPlayer!.Move(Direction.Up)
                        ), ConsoleKey.A, () => Game.GetPlayer!.Move(Direction.Left)
                    ), ConsoleKey.S, () => Game.GetPlayer!.Move(Direction.Down)
                ), ConsoleKey.D, () => Game.GetPlayer!.Move(Direction.Right)
            ), ConsoleKey.Escape, Game.GetGame!.Stop
        );
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
        _keyControl = new KeyControl(_keyControl, ConsoleKey.E, () => Game.GetPlayer!.PickUp());
        _hasItems = true;
    }

    public void AddWeapons(int n = 1)
    {
        if (_hasWeapons || n == 0) return;
        if(!_hasUsables) AddCurrencies();
        _keyControl = new KeyControl(_keyControl, ConsoleKey.R, () => Game.GetPlayer!.Use());
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
        _keyControl = new KeyControl(_keyControl, ConsoleKey.R, () => Game.GetPlayer!.Use());
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