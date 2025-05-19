namespace ProjOb;

public class Goblin : Enemy
{
    public Goblin(Tile position) : base("Goblin", 'g', position)
    {
        SetStats(5,5,5,5,5,10, 5);
        var smallsword = new SmallSword();
        Inventory.Grab(smallsword);
        smallsword.OnPickUp(this);
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