using System.Collections;
using System.Drawing;

namespace ProjOb;

public class Tile : IEnumerable<IMappable>
{
    public readonly Map Map;
    private readonly List<IMappable> _items = [];
    public Point Position {get; set;}
    public bool IsWalkable {get; set;}
    public char Print() => _items.Count == 0 ? (IsWalkable ? ' ' : '\u2588') : _items[^1].Display;
    public event Action? OnUpdate;

    public Tile(Map map, bool isWalkable, Point position)
    {
        Map = map;
        IsWalkable = isWalkable;
        Position = position;
    }

    public void Add(IMappable item)
    {
        _items.Add(item);
        OnUpdate?.Invoke();
    }

    public IMappable? Pick()
    {
        if (_items.Count > 1)
        {
            OnUpdate?.Invoke();
            var ret = _items[^2];
            _items.RemoveAt(_items.Count - 2);
            return ret;
        }
        return null;
    }

    public void Remove(IMappable item)
    {
        _items.Remove(item);
    }
    
    public IEnumerator<IMappable> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}