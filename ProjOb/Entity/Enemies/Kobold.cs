namespace ProjOb;

public class Kobold : Enemy
{
    public Kobold(Tile position) : base("Kobold", 'k', position)
    {
        SetStats(7,7,7,7,7,20, 7);
        Grab(new Dagger());
        HeldItems[0]?.OnPickUp(this);
    }

    public override void Attack(int _, Entity target)
    {
        HeldItems[0]?.ToWeapon()?.Attack(new HiddenAttack(this, target));
    }
}