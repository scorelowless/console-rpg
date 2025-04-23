namespace ProjOb;

public class Ogre : Enemy
{
    public Ogre(Tile position) : base("Ogre", 'o', position)
    {
        SetStats(10,10,10,10,10,100, 10);
        Grab(new Weak(new Longsword()));
        HeldItems[0]?.OnPickUp(this);
    }

    public override void Attack(int _, Entity target)
    {
        HeldItems[0]?.ToWeapon()?.Attack(new NormalAttack(this, target));
    }
}