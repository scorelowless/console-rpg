namespace ProjOb;

public class Mage : Enemy
{
    public int StrategyCounter {get; set;}
    public Mage(Tile position, int index) : base("Mage", 'm', position, index)
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

    protected override void StrategyUpdate(Player _)
    {
        StrategyCounter = (StrategyCounter + 1) % 9;
        Strategy = StrategyCounter switch
        {
            < 3 => new EnemyStrategyCalm(),
            < 6 => new EnemyStrategyAggressive(),
            < 9 => new EnemyStrategyAfraid(),
            _ => throw new Exception("Unexpected behavior in Mage.StrategyUpdate")
        };
    }
}