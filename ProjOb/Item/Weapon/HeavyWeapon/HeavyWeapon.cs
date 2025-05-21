namespace ProjOb;

public abstract class HeavyWeapon : Weapon
{
    protected HeavyWeapon(string name, char display) : base(name, display)
    {
    }

    public override int Attack(IAttackVisitor v)
    {
        return v.AttackHeavy(this);
    }
    
    public override void OnPickUp(Entity entity)
    {
        Damage = entity.Stats[Entity.StatsType.Power] + entity.Stats[Entity.StatsType.Aggression];
        base.OnPickUp(entity);
    }
}