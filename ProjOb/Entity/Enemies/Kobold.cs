namespace ProjOb;

public class Kobold : Enemy
{
    public Kobold(Tile position) : base("Kobold", 'k', position)
    {
        SetStats(7,7,7,7,7,20, 7);
        var dagger = new Dagger();
        Inventory.Grab(dagger);
        dagger.OnPickUp(this);
    }

    protected override int Attack(int _, Entity target)
    {
        foreach (IHeldable heldable in Inventory.HeldItemsList)
        {
            heldable.ToWeapon()?.Attack(new HiddenAttack(this, target));
        }
        return ReturnCode.SUCCESS;
    }
}