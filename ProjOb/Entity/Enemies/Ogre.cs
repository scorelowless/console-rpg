namespace ProjOb;

public class Ogre : Enemy
{
    public Ogre(Tile position) : base("Ogre", 'o', position)
    {
        SetStats(20,20,20,20,20,100, 10);
        Grab(new Weak(new Longsword()));
    }

    public override void Attack(int _, Entity target)
    {
        HeldItems[0]?.ToWeapon()?.Attack(new NormalAttack(this, target));
    }
}