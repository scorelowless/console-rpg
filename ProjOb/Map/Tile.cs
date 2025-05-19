using System.Collections;
using System.Drawing;

namespace ProjOb;

public class Tile : IEnumerable<IMappable>
{
    public readonly Map Map;
    private readonly List<IItem> _items = [];
    private readonly List<Enemy> _enemies = [];
    private Player? _player;
    public Point Position { get; }
    public bool IsNotWall { get; set; }
    public ColoredChar Print()
    {
        if (_player != null) return _player.Display;
        if (_enemies.Count != 0) return _enemies[^1].Display;
        if(_items.Count != 0) return _items[0].Display;
        if (!IsNotWall) return new ColoredChar('\u2588');
        return new ColoredChar(' ');
    }

    public Tile(Map map, bool isNotWall, Point position)
    {
        Map = map;
        IsNotWall = isNotWall;
        Position = position;
    }

    public void AddItem(IItem item)
    {
        _items.Add(item);
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void AddPlayer(Player player)
    {
        _player = player;
    }

    public IItem? Pick(int ind)
    {
        if(_items.Count <= ind) return null;
        IItem item = _items[ind];
        _items.RemoveAt(ind);
        return item;
    }

    public void RemoveItem(IItem item)
    {
        _items.Remove(item);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    public void RemovePlayer()
    {
        _player = null;
    }
    
    public Enemy? ContainsEnemies() => _enemies.Count == 0 ? null : _enemies[0];

    public bool ContainsItems => _items.Count != 0;
    
    public IEnumerator<IMappable> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}