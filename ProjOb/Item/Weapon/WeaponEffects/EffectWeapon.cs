namespace ProjOb.WeaponEffects;

public abstract class EffectWeapon : IWeapon
{
    protected Weapon Weapon;
    private readonly string _effectName;
    protected Action OnGrabCustom = () => { };
    protected Action OnUngrabCustom = () => { };

    public Entity? Owner
    {
        get => Weapon.Owner;
        set => Weapon.Owner = value;
    }
    public Action OnGrab => Weapon.OnGrab + OnGrabCustom;
    public Action OnUngrab => Weapon.OnUngrab + OnUngrabCustom;
    public bool IsHeld
    {
        get => Weapon.IsHeld;
        set => Weapon.IsHeld = value;
    }
    public int HandsTaken => Weapon.HandsTaken;
    public virtual int Damage => Weapon.Damage;
    public string Name => Weapon.Name + _effectName;


    public EffectWeapon(Weapon weapon, string effectName)
    {
        Weapon = weapon;
        _effectName = effectName;
    }

    public virtual Weapon RemoveEffect()
    {
        return Weapon;
    }
}