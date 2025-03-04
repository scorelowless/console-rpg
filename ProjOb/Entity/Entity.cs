using System.Drawing;

namespace ProjOb;

public class Entity : IMappable
{
    public Point Position { get; set; }
    public char Display { get; set; }
    public Map Map { get; set; }
    public Attributes Stats { get; set; }
    public IHeldable[] HeldItems { get; set; }

    public Item[] Inventory { get; set; }

    public Entity(Map map)
    {
        Map = map;
    }
}