using System.Collections;
using System.Drawing;

namespace ProjOb;

public class Tile : IEnumerable<IMappable>
{
    public readonly Map Map;
    private readonly List<IMappable> _contents = [];
    public Point Position { get; }
    public bool IsWalkable { get; }
    public char Print() => _contents.Count == 0 ? (IsWalkable ? ' ' : '\u2588') : _contents[^1].Display;
    public event Action? OnUpdate;

    public Tile(Map map, bool isWalkable, Point position)
    {
        Map = map;
        IsWalkable = isWalkable;
        Position = position;
    }

    public void Add(IMappable item)
    {
        _contents.Add(item);
        OnUpdate?.Invoke();
    }

    public IItem? Pick()
    {
        foreach (var item in _contents)
        {
            if (item.ToItem() == null) continue;
            _contents.Remove(item);
            OnUpdate?.Invoke();
            return item.ToItem();
        }
        return null;
    }

    public void Remove(IMappable item)
    {
        _contents.Remove(item);
        OnUpdate?.Invoke();
    }
    
    public IEnumerator<IMappable> GetEnumerator()
    {
        return _contents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}