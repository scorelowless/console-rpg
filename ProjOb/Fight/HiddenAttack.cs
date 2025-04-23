namespace ProjOb;

public class HiddenAttack : IAttackVisitor
{
    private readonly Entity _attacker;
    private readonly Entity _target;
    
    public int Armor { get; private set; }

    public HiddenAttack(Entity attacker, Entity target)
    {
        _attacker = attacker;
        _target = target;
    }

    public void AttackHeavy(IWeapon w)
    {
        _target.ReceiveDamage(w.Damage / 2);
        Armor = _attacker.Stats[Entity.StatsType.Power];
    }

    public void AttackLight(IWeapon w)
    {
        _target.ReceiveDamage(w.Damage * 2);
        Armor = _attacker.Stats[Entity.StatsType.Agility];
    }

    public void AttackMagic(IWeapon w)
    {
        _target.ReceiveDamage(1);
    }

}