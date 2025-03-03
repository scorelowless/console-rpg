using System.Drawing;

namespace ProjOb;

public class Item : IMappable
{
    public Point Position { get; set; }
    public char Display { get; set; }
    public string Name { get; set; }

    public Item(Point position = default, string name = "NONAME", char display = '¿')
    {
        Name = name;
        Display = display;
        Position = position;
    }
}