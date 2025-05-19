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

    protected override IActionType Attack(int _, Entity target)
    {
        foreach (IHeldable heldable in Inventory.HeldItemsList)
        {
            heldable.ToWeapon()?.Attack(new NormalAttack(this, target));
        }
        return new ActionType.Attack(this, target);
    }
}