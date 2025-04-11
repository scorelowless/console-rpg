namespace ProjOb;

public abstract class EffectWeapon : IWeapon
{
    protected readonly IWeapon Weapon;
    private readonly string _effectName;

    public string Info => $"Damage: {Damage}";
    public Entity? Owner => Weapon.Owner;
    public virtual void OnPickUp(Entity entity)
    {
        Weapon.OnPickUp(entity);
    }

    public virtual void OnThrow()
    {
        Weapon.OnThrow();
    }

    public virtual (bool, IItem?) OnUse()
    {
        Weapon.OnUse();
        return (true, this);
    }

    public virtual void OnUnequip()
    {
        Weapon.OnUnequip();
    }
    public bool IsHeld
    {
        get => Weapon.IsHeld;
        set => Weapon.IsHeld = value;
    }
    public int HandsTaken => Weapon.HandsTaken;
    public virtual int Damage => Weapon.Damage;

    public ColoredChar Display { get; }
    public string Name => Weapon.Name + _effectName;

    public IHeldable ToHeldable() => this;


    protected EffectWeapon(IWeapon weapon, string effectName)
    {
        Weapon = weapon;
        _effectName = effectName;
        Display = new ColoredChar(Weapon.Display.Character, ConsoleColor.Cyan);
    }

    public virtual IWeapon RemoveEffect()
    {
        return Weapon;
    }
}