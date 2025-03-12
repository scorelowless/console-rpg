namespace ProjOb.WeaponEffects;

public abstract class EffectWeapon : IWeapon
{
    protected readonly IWeapon Weapon;
    private readonly string _effectName;
    protected Action OnGrabCustom = () => { };
    protected Action OnUngrabCustom = () => { };

    public Entity? Owner => Weapon.Owner;
    public Action OnGrab => Weapon.OnGrab + OnGrabCustom;
    public Action OnUngrab => Weapon.OnUngrab + OnUngrabCustom;
    public bool IsHeld
    {
        get => Weapon.IsHeld;
        set => Weapon.IsHeld = value;
    }
    public int HandsTaken => Weapon.HandsTaken;
    public virtual int Damage => Weapon.Damage;

    public Tile Position
    {
        get => Weapon.Position;
        set => Weapon.Position = value;
    }

    public char Display
    {
        get => Weapon.Display;
        set => Weapon.Display = value;
    }
    public string Name => Weapon.Name + _effectName;
    public Action<Entity> OnPickUp => Weapon.OnPickUp;
    public Action<Entity> OnThrow => Weapon.OnThrow;


    public EffectWeapon(IWeapon weapon, string effectName)
    {
        Weapon = weapon;
        _effectName = effectName;
    }

    public virtual IWeapon RemoveEffect()
    {
        return Weapon;
    }
}