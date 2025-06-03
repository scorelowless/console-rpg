namespace ProjOb;

public class Kobold : Enemy
{
    public Kobold(Tile position, int index) : base("Kobold", 'k', position, index)
    {
        SetStats(7,7,7,7,7,20, 7);
        var dagger = new Dagger();
        Inventory.Grab(dagger);
        dagger.OnPickUp(this);
        Strategy = new EnemyStrategyAggressive();
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

    protected override void StrategyUpdate(Player _)
    {
        if (Stats[StatsType.Health] < 10)
        {
            Strategy = new EnemyStrategyAfraid();
        }
    }
}