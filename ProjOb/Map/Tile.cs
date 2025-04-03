using System.Collections;
using System.Drawing;

namespace ProjOb;

public class Tile : IEnumerable<IMappable>
{
    public readonly Map Map;
    private readonly List<IItem> _items = [];
    private readonly List<Enemy> _enemies = [];
    private bool _hasPlayer;
    public Point Position { get; }
    public bool IsNotWall { get; set; }
    public char Print()
    {
        if (_hasPlayer) return '¶';
        if (_enemies.Count != 0) return _enemies[^1].Display;
        if(_items.Count != 0) return _items[0].Display;
        if (!IsNotWall) return '\u2588';
        return ' ';
    }
    public event Action? OnUpdate;

    public Tile(Map map, bool isNotWall, Point position)
    {
        Map = map;
        IsNotWall = isNotWall;
        Position = position;
    }

    public void AddItem(IItem item)
    {
        _items.Add(item);
        OnUpdate?.Invoke();
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        OnUpdate?.Invoke();
    }

    public void AddPlayer()
    {
        _hasPlayer = true;
        OnUpdate?.Invoke();
    }

    public IItem? Pick()
    {
        if(_items.Count == 0) return null;
        IItem item = _items[0];
        _items.Remove(item);
        OnUpdate?.Invoke();
        return item;
    }

    public void RemoveItem(IItem item)
    {
        _items.Remove(item);
        OnUpdate?.Invoke();
    }
    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        OnUpdate?.Invoke();
    }

    public void RemovePlayer()
    {
        _hasPlayer = false;
        OnUpdate?.Invoke();
    }
    
    public Enemy? ContainsEnemies()
    {
        return _enemies.Count == 0 ? null : _enemies[0];
    }

    public bool ContainsItems => _items.Count != 0;
    
    public IEnumerator<IMappable> GetEnumerator()
    {
        return _enemies.Count != 0 ? _enemies.GetEnumerator() : _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}