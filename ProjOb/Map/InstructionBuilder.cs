using System.Text;

namespace ProjOb;

public class InstructionBuilder : IMapBuilder
{
    private bool _hasItems = false;
    private bool _hasUsables = false;
    private bool _hasWeapons = false;
    private bool _hasEnemies = false;
    private readonly StringBuilder _text = new StringBuilder();
    public void Reset()
    {
        _text.Clear();
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

    public void AddPaths(int n = 1)
    {
    }

    public void AddDefaultPath()
    {
    }

    public void AddRooms(int n = 1)
    {
    }

    public void AddMainRoom()
    {
    }

    public void AddItems(int n = 1)
    {
        if (_hasItems || n == 0) return;
        _text.Append("E: pick up item\n");
        _text.Append("Q: drop item\n");
        _hasItems = true;
    }

    public void AddWeapons(int n = 1)
    {
        if (_hasWeapons || n == 0) return;
        if(!_hasUsables) AddCurrencies();
        _text.Append("T: unequip weapon\n");
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
        _text.Append("R: use/equip item\n");
        _hasUsables = true;
    }

    public void AddEnemies(int n = 1)
    {
        if(_hasEnemies || n == 0) return;
        _text.Append("X: attack nearby enemy(not implemented)\n");
    }

    public object GetResult()
    {
        return _text.ToString();
    }
}