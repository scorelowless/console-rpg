namespace ProjOb;

public class Ogre : Enemy
{
    public Ogre(Tile position, int index) : base("Ogre", 'o', position, index)
    {
        SetStats(10,10,10,10,10,100, 10);
        var longsword = new Weak(new Longsword());
        Inventory.Grab(longsword);
        longsword.OnPickUp(this);
        Strategy = new EnemyStrategyAggressive();
    }
    
    public Ogre()
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

    protected override void StrategyUpdate(Player player)
    {
        int distance = Position.Map.CheckDistance(Position, player.Position);
        if (distance > 3) return;
        if(Stats[StatsType.Power] > player.Stats[StatsType.Power])
        {
            Strategy = new EnemyStrategyAggressive();
        }
        else
        {
            Strategy = new EnemyStrategyCalm();
        }
    }
}