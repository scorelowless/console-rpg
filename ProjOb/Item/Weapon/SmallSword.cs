namespace ProjOb;

public class SmallSword : Weapon
{
    public SmallSword(Tile? position) : base("SmallSword", 's', position)
    {
        Damage = 7;
        HandsTaken = 1;
    }
}