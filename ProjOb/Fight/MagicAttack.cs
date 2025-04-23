namespace ProjOb;

public class MagicAttack : IAttackVisitor
{
    private readonly Entity _attacker;
    private readonly Entity _target;
    
    public int Armor { get; private set; }

    public MagicAttack(Entity attacker, Entity target)
    {
        _attacker = attacker;
        _target = target;
        Armor = attacker.Stats[Entity.StatsType.Luck];
    }
    public void AttackHeavy(IWeapon w)
    {
        _target.ReceiveDamage(1);
    }

    public void AttackLight(IWeapon w)
    {
        _target.ReceiveDamage(1);
    }

    public void AttackMagic(IWeapon w)
    {
        _target.ReceiveDamage(w.Damage);
        Armor = 2 * _attacker.Stats[Entity.StatsType.Wisdom];
    }
}