namespace ProjOb;

public class Staff : Weapon
{
    public Staff() : base("Staff", 's')
    {
        Damage = 10;
        HandsTaken = 1;
        Type = WeaponType.Magic;
    }
}