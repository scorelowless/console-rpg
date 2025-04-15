namespace ProjOb;

public class SmallSword : Weapon
{
    public SmallSword() : base("SmallSword", 's')
    {
        Damage = 7;
        HandsTaken = 1;
        Type = WeaponType.Light;
    }
}