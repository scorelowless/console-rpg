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
    
    public Kobold()
    {
    }

    protected override IResultType Attack(int _, Entity target)
    {
        int damage = 0;
        foreach (IHeldable heldable in Inventory.HeldItemsList)
        {
            damage += heldable.ToWeapon()?.Attack(new HiddenAttack(this, target)) ?? 0;
        }
        return new ResultType.Attack(this, target, damage);
    }
}