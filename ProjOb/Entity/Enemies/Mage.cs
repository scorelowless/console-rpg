namespace ProjOb;

public class Mage : Enemy
{
    public Mage(Tile position) : base("Mage", 'm', position)
    {
        Grab(new Staff());
    }
}