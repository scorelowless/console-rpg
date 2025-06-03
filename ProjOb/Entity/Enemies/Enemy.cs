using System.Drawing;
using System.Text.Json.Serialization;

namespace ProjOb;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Goblin), "Goblin")]
[JsonDerivedType(typeof(Kobold), "Kobold")]
[JsonDerivedType(typeof(Mage), "Mage")]
[JsonDerivedType(typeof(Ogre), "Ogre")]
public abstract class Enemy : Entity
{
    public int EnemyIndex { get; set; }
    public IEnemyStrategy Strategy { get; set; } = new EnemyStrategyCalm();
    protected Enemy(string name, char display, Tile position, int index) : base(name, display, position, ConsoleColor.DarkRed)
    {
        position.AddEnemy(this);
        EnemyIndex = index;
    }
    
    public Enemy()
    {
    }

    public override void ReceiveDamage(int damage)
    {
        Stats[StatsType.Health] -= int.Max(damage - Stats[StatsType.Armor], 0);
        if (Stats[StatsType.Health] <= 0)
        {
            Position.RemoveEnemy(this);
            Position.Map.Enemies[EnemyIndex] = null;
        }
        Strategy = new EnemyStrategyAttacked();
    }
    
    public override IResultType Move(Direction direction)
    {
        Tile nextPosition = Position.Map.NextTile(Position, direction);
        if (nextPosition == Position || nextPosition.ContainsEnemies() != null || nextPosition.ContainsPlayer())
            return ResultType.Unsuccessful.CantMove();
        Position.Enemies.Remove(this);
        Position = nextPosition;
        Position.Enemies.Add(this);
        return new ResultType.Move(direction, null);
    }

    protected abstract void StrategyUpdate(Player player);
    
    public IResultType? ExecuteStrategy(Player player)
    {
        StrategyUpdate(player);
        return Strategy.Execute(this, player);
    }
    public IResultType Attack(Entity target)
    {
        return Attack(-1, target);
    }
}