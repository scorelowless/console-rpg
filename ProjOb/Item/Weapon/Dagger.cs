namespace ProjOb;

public class Dagger : Weapon
{
    public Dagger(Tile? position) : base("Dagger", 'd', position)
    {
        Damage = 5;
        HandsTaken = 1;
    }
}