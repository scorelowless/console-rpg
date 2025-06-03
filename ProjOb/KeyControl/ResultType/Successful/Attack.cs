namespace ProjOb.ResultType;

public class Attack : IResultType
{
    private readonly int _damage;
    public readonly Entity Target;
    private readonly Entity _attacker;
    public Attack(Entity attacker, Entity target, int damage)
    {
        _damage = damage;
        Target = target;
        _attacker = attacker;
    }
    public bool WasAttack => true;
    public string Message => $"{_attacker.Name} dealt {_damage} damage to {Target.Name}{(Target.Stats[Entity.StatsType.Health] <= 0 ? " and killed it!" : "")}";
}