namespace ProjOb;

public abstract class Weapon : Item, IWeapon
{
    public Entity? Owner { get; set; }
    public Action OnGrab { get; protected set; }
    public Action OnUngrab { get; protected set; }
    public bool IsHeld { get; set; }
    public int HandsTaken { get; init; }
    public int Damage { get; protected set; }

    protected Weapon(string name, char display, Tile? position = null) : base(name, display, position)
    {
        OnGrab = () => IsHeld = true;
        OnUngrab = () => IsHeld = false;
        OnPickUp = entity => Owner = entity;
        OnThrow = entity => Owner = null;
    }
}