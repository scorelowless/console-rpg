namespace ProjOb;

public abstract class Weapon : Item, IWeapon
{
    public bool IsHeld { get; set; }
    public int HandsTaken { get; protected init; }
    public int Damage { get; protected init; }
    public override string Info => $"Damage: {Damage}";
    public override IHeldable ToHeldable() => this;

    protected Weapon(string name, char display) : base(name, display)
    {
        OnUse = () => (IsHeld = true, this);
        OnUnequip += () => IsHeld = false;
        OnPickUp += entity => Owner = entity;
        OnThrow += () => Owner = null;
    }
}