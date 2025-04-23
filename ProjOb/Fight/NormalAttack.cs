namespace ProjOb;

public class NormalAttack : IAttackVisitor
{
    private readonly Entity _attacker;
    private readonly Entity _target;
    
    public int Armor { get; private set; }

    public NormalAttack(Entity attacker, Entity target)
    {
        _attacker = attacker;
        _target = target;
        Armor = attacker.Stats[Entity.StatsType.Agility];
    }
    public void AttackHeavy(IWeapon w)
    {
        _target.ReceiveDamage(w.Damage);
        Armor = _attacker.Stats[Entity.StatsType.Power] + _attacker.Stats[Entity.StatsType.Luck];
    }

    public void AttackLight(IWeapon w)
    {
        _target.ReceiveDamage(w.Damage);
        Armor = _attacker.Stats[Entity.StatsType.Agility] + _attacker.Stats[Entity.StatsType.Luck];
    }

    public void AttackMagic(IWeapon w)
    {
        _target.ReceiveDamage(1);
        Armor = _attacker.Stats[Entity.StatsType.Agility] + _attacker.Stats[Entity.StatsType.Luck];
    }
}