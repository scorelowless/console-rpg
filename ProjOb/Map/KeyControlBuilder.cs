using ProjOb.ActionType;

namespace ProjOb;

public class KeyControlBuilder : IMapBuilder
{
    private bool _hasItems;
    private bool _hasUsables;
    private bool _hasWeapons;
    private bool _hasEnemies;
    private IKeyControl _keyControl = null!;
    private Player _player; // TODO: won't do in multiplayer

    public KeyControlBuilder(Player player)
    {
        Reset();
        _player = player;
    }
    
    public void Reset()
    {
        _keyControl = new GuardKeyControl(_player);
        _keyControl = new KeyControl(_keyControl, ConsoleKey.W, () => _player.Move(Direction.Up));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.A, () => _player.Move(Direction.Left));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.S, () => _player.Move(Direction.Down));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.D, () => _player.Move(Direction.Right));
        _keyControl = new KeyControl(_keyControl, ConsoleKey.Escape, () => _player.Die());
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
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.E, _player.PickUp, new PickUpPrompt(_player));
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.Q, _player.ThrowAway, new ThrowAwayPrompt(_player));
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
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.R, _player.Use, new UsePrompt(_player));
        _hasUsables = true;
    }

    public void AddEnemies(int n = 1)
    {
        if(_hasEnemies || n == 0) return;
        _keyControl = new NumberKeyControl(_keyControl, ConsoleKey.X, _player.Attack, new AttackTypePrompt(_player));
        _hasEnemies = true;
    }

    public object GetResult()
    {
        return _keyControl;
    }
}