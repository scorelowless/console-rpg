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
        
    }

    public override void OnThrow() => Owner = null;
    public override void OnUnequip() => IsHeld = false;
    public override (bool, IItem?) OnUse() => (IsHeld = true, this);
}