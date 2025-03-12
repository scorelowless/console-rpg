using System.Drawing;

namespace ProjOb;

public class Tile
{
    private List<IMappable> _items = [];
    public Point Position {get; set;}
    public bool IsWalkable {get; set;}
    public char GetDisplay()
    {
        return _items.Count == 0 ? (IsWalkable ? ' ' : '\u2588') : _items[0].Display;
    }

    public Tile(bool isWalkable, Point position)
    {
        IsWalkable = isWalkable;
        Position = position;
    }
}