using System.Drawing;

namespace ProjOb;

public abstract class Item : IMappable
{
    public Tile? Position { get; set; }
    public char Display { get; set; }
    public string Name { get; init; }
    public Action<Entity> OnPickUp { get; protected init; } = _ => { };
    public Action<Entity> OnThrow { get; protected init; } = _ => { };

    protected Item(string name, char display, Tile? position = null)
    {
        Position = position;
        Name = name;
        Display = display;
    }
}