namespace ProjOb;

public interface IWeapon : IHeldable
{
    public int Damage { get; }
    public Weapon.WeaponType Type { get; }
}