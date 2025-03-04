namespace ProjOb.WeaponEffects;

public class Weak : EffectWeapon
{
    private const int Value = 5;
    public override int Damage => Weapon.Damage + Value;

    public Weak(Weapon weapon) : base(weapon, " (Weak)")
    {
    }
}