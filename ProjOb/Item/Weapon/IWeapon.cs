namespace ProjOb;

public interface IWeapon : IHeldable
{
    public int Damage { get; }

    public void Attack(IAttackVisitor v);
}