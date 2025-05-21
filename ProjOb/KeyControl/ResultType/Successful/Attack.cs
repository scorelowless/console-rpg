namespace ProjOb.ResultType;

public class Attack : IResultType
{
    private readonly int _damage;
    private readonly Entity _target;
    private readonly Entity _attacker;
    public Attack(Entity attacker, Entity target, int damage)
    {
        _damage = damage;
        _target = target;
        _attacker = attacker;
    }
    public bool WasSuccessful => true;
    public bool IsSenderDead => false;
    public string Message => $"{_attacker.Name} dealt {_damage} damage to {_target.Name}";
}