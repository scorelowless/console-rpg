using System.Drawing;

namespace ProjOb;

public interface IMappable
{
    Tile Position { get; set; }
    char Display { get; set; }
    string Name { get; }
    Action<Entity> OnPickUp { get; }
    Action<Entity> OnThrow { get; }
}