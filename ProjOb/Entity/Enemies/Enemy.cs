namespace ProjOb;

public abstract class Enemy : Entity
{
    protected Enemy(string name, char display, Tile position) : base(name, display, position, ConsoleColor.DarkRed)
    {
        position.AddEnemy(this);
    }

    public override void ReceiveDamage(int damage)
    {
        Stats[StatsType.Health] -= int.Max(damage - Stats[StatsType.Armor], 0);
        if (Stats[StatsType.Health] <= 0)
        {
            Position.RemoveEnemy(this);
        }
    }

    public void Attack(Entity target)
    {
        Attack(-1, target);
    }
}