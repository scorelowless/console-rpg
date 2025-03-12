using System.Drawing;

namespace ProjOb;

public class Stone : Item
{
    public Stone(Tile? position) : base( "Stone", 's', position)
    {
    }
}