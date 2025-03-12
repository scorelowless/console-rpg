namespace ProjOb;

public class Longsword : Weapon
{
    public Longsword(Tile? position) : base("Longsword", 'l', position)
    {
        HandsTaken = 2;
        Damage = 15;
    }
}