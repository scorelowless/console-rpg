namespace ProjOb;

public class Goblin : Enemy
{
    public Goblin(Tile position, int index) : base("Goblin", 'g', position, index)
    {
        SetStats(5,5,5,5,5,10, 5);
        var smallsword = new SmallSword();
        Inventory.Grab(smallsword);
        smallsword.OnPickUp(this);
        Strategy = new EnemyStrategyAfraid();
    }
    
    public Goblin()
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

    protected override void StrategyUpdate(Player _)
    {
    }
}