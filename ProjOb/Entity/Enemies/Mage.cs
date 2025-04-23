namespace ProjOb;

public class Mage : Enemy
{
    public Mage(Tile position) : base("Mage", 'm', position)
    {
        SetStats(10,10,10,10,10,30, 5);
        Grab(new Staff());
    }

    public override void Attack(int _, Entity target)
    {
        HeldItems[0]?.ToWeapon()?.Attack(new MagicAttack(this, target));
    }
}