namespace ProjOb;

public abstract class Weapon : Item, IWeapon
{
    public bool IsHeld { get; set; }
    public int HandsTaken { get; protected init; }
    public int Damage { get; protected init; }
    public WeaponType Type { get; protected init; } = WeaponType.Undefined;
    public override string Info => $"Damage: {Damage}";
    public override IHeldable ToHeldable() => this;

    protected Weapon(string name, char display) : base(name, display, ConsoleColor.Green)
    {
        
    }

    public override void OnThrow() => Owner = null;
    public override void OnUnequip() => IsHeld = false;
    public override (bool, IItem?) OnUse() => (IsHeld = true, this);

    public enum WeaponType
    {
        Undefined = 0,
        Heavy,
        Light,
        Magic
    }
}