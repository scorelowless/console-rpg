namespace ProjOb;

public abstract class Enemy : Entity
{
    protected Enemy(string name, char display, Tile position) : base(name, display, position, ConsoleColor.DarkRed)
    {
        position.AddEnemy(this);
        Stats = new Dictionary<StatsType, int>
        {
            { StatsType.Health, 0 },
            { StatsType.Attack, 0 },
            { StatsType.Armor, 0 }
        };
    }
}