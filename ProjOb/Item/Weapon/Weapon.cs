namespace ProjOb;

public abstract class Weapon : Item, IWeapon
{
    public bool IsHeld { get; set; }
    public int HandsTaken { get; set; }
    public int Damage { get; set; }

    public override string Info => $"Damage: {Damage}";
    public override IHeldable ToHeldable() => this;

    protected Weapon(string name, char display) : base(name, display, ConsoleColor.Green)
    {
        
    }

    public override void OnThrow() => Owner = null;
    public override void OnUnequip() => IsHeld = false;

    public override (bool, IItem?) OnUse() => (IsHeld = true, this);

    public abstract int Attack(IAttackVisitor v);

    public IWeapon ToWeapon() => this;
}