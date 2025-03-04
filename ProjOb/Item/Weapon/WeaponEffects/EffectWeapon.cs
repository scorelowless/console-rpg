namespace ProjOb.WeaponEffects;

public abstract class EffectWeapon : Weapon
{
    protected Weapon Weapon;
    protected readonly string EffectName;
    public override string Name => Weapon.Name + EffectName;

    public EffectWeapon(Weapon weapon, string effectName)
    {
        Weapon = weapon;
        EffectName = effectName;
    }
}