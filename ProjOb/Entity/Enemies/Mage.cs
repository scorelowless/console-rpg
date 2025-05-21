namespace ProjOb;

public class Mage : Enemy
{
    public Mage(Tile position) : base("Mage", 'm', position)
    {
        SetStats(10,10,10,10,10,30, 5);
        var staff = new Staff();
        Inventory.Grab(staff);
        staff.OnPickUp(this);
    }
    
    public Mage()
    {
    }

    protected override IResultType Attack(int _, Entity target)
    {
        int damage = 0;
        foreach (IHeldable heldable in Inventory.HeldItemsList)
        {
            damage += heldable.ToWeapon()?.Attack(new MagicAttack(this, target)) ?? 0;
        }
        return new ResultType.Attack(this, target, damage);
    }
}