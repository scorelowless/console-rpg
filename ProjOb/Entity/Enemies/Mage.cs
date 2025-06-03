namespace ProjOb;

public class Mage : Enemy
{
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
        int walls = 0;
        if(!Position.Map.NextTile(Position, Direction.Up).IsNotWall) walls++;
        if(!Position.Map.NextTile(Position, Direction.Right).IsNotWall) walls++;
        if(!Position.Map.NextTile(Position, Direction.Down).IsNotWall) walls++;
        if(!Position.Map.NextTile(Position, Direction.Left).IsNotWall) walls++;
        Strategy = walls switch
        {
            0 => new EnemyStrategyCalm(),
            1 => new EnemyStrategyAggressive(),
            2 => new EnemyStrategyAfraid(),
            3 => new EnemyStrategyAggressive(),
            4 => new EnemyStrategyCalm(),
            _ => throw new Exception("Unexpected behavior in Mage.StrategyUpdate")
        };
    }
}