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

    protected override int Attack(int _, Entity target)
    {
        foreach (IHeldable heldable in Inventory.HeldItemsList)
        {
            heldable.ToWeapon()?.Attack(new MagicAttack(this, target));
        }
        return ReturnCode.SUCCESS;
    }
}