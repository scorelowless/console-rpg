using System.Drawing;

namespace ProjOb;

public class Tile
{
    private List<IMappable> _items = [];
    public Point Position {get; set;}
    public char GetDisplay()
    {
        return _items.Count == 0 ? ' ' : _items[0].Display;
    }
}