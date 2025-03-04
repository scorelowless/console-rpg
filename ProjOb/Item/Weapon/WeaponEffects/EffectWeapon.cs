namespace ProjOb.WeaponEffects;

public abstract class EffectWeapon : Weapon
{
    protected Weapon Weapon;
    private readonly string _effectName;
    public override string Name => Weapon.Name + _effectName;
    protected Action WhenGrabbed;
    protected Action WhenUngrabbed;

    public EffectWeapon(Weapon weapon, string effectName)
    {
        Weapon = weapon;
        _effectName = effectName;
    }

    public virtual Weapon RemoveEffect()
    {
        if(IsHeld)
            WhenUngrabbed();
        return Weapon;
    }
}