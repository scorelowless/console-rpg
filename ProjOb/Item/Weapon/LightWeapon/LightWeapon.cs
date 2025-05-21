namespace ProjOb;

public abstract class LightWeapon : Weapon
{
    protected LightWeapon(string name, char display) : base(name, display)
    {
    }

    public override int Attack(IAttackVisitor v)
    {
        return v.AttackLight(this);
    }

    public override void OnPickUp(Entity entity)
    {
        Damage = entity.Stats[Entity.StatsType.Agility] + entity.Stats[Entity.StatsType.Luck];
        base.OnPickUp(entity);
    }
}