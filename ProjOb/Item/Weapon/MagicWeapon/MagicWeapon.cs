namespace ProjOb;

public abstract class MagicWeapon : Weapon
{
    protected MagicWeapon(string name, char display) : base(name, display)
    {
    }

    public override void Attack(IAttackVisitor v)
    {
        v.AttackMagic(this);
    }
    
    public override void OnPickUp(Entity entity)
    {
        Damage = (int)(1.5f * entity.Stats[Entity.StatsType.Power]);
        base.OnPickUp(entity);
    }
}