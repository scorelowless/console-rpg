namespace ProjOb;

public interface IWeapon : IHeldable, IMappable
{
    public int Damage { get; }
    new string Name { get; }
}