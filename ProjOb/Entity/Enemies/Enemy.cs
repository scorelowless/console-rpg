using System.Text.Json.Serialization;

namespace ProjOb;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Goblin), "Goblin")]
[JsonDerivedType(typeof(Kobold), "Kobold")]
[JsonDerivedType(typeof(Mage), "Mage")]
[JsonDerivedType(typeof(Ogre), "Ogre")]
public abstract class Enemy : Entity
{
    protected Enemy(string name, char display, Tile position) : base(name, display, position, ConsoleColor.DarkRed)
    {
        position.AddEnemy(this);
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
        }
    }

    public IResultType Attack(Entity target)
    {
        return Attack(-1, target);
    }
}