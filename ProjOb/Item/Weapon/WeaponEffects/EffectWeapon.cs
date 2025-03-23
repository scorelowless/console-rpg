namespace ProjOb.WeaponEffects;

public abstract class EffectWeapon : IWeapon
{
    protected readonly IWeapon Weapon;
    private readonly string _effectName;
    protected Func<(bool, IItem?)> OnUseCustom;
    protected Action OnUnequipCustom = () => { };

    public string Info => $"Damage: {Damage}";
    public Entity? Owner => Weapon.Owner;
    public Func<(bool, IItem?)> OnUse => Weapon.OnUse + OnUseCustom;
    public Action OnUnequip => OnUnequipCustom + Weapon.OnUnequip;
    public bool IsHeld
    {
        get => Weapon.IsHeld;
        set => Weapon.IsHeld = value;
    }
    public int HandsTaken => Weapon.HandsTaken;
    public virtual int Damage => Weapon.Damage;

    public Tile? Position
    {
        get => Weapon.Position;
        set => Weapon.Position = value;
    }

    public char Display => Weapon.Display;
    public string Name => Weapon.Name + _effectName;
    
    public IItem ToItem() => this;
    public IHeldable ToHeldable() => this;

    public Action<Entity> OnPickUp => Weapon.OnPickUp;
    public Action OnThrow => Weapon.OnThrow;


    protected EffectWeapon(IWeapon weapon, string effectName)
    {
        Weapon = weapon;
        _effectName = effectName;
        OnUseCustom = () => (true, this);
    }

    public virtual IWeapon RemoveEffect()
    {
        return Weapon;
    }
}