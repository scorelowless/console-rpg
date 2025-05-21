namespace ProjOb;

public class Ogre : Enemy
{
    public Ogre(Tile position) : base("Ogre", 'o', position)
    {
        SetStats(10,10,10,10,10,100, 10);
        var longsword = new Weak(new Longsword());
        Inventory.Grab(longsword);
        longsword.OnPickUp(this);
    }
    
    public Ogre()
    {
    }

    protected override IResultType Attack(int _, Entity target)
    {
        int damage = 0;
        foreach (IHeldable heldable in Inventory.HeldItemsList)
        {
            damage += heldable.ToWeapon()?.Attack(new NormalAttack(this, target)) ?? 0;
        }
        return new ResultType.Attack(this, target, damage);
    }
}