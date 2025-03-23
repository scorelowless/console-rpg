namespace ProjOb;

public abstract class Item : IItem
{
    public Tile? Position { get; set; }
    public char Display { get; set; }
    public virtual string Name { get; }
    public virtual string Info { get; protected init; } = "";
    public IItem ToItem() => this;
    public virtual IHeldable? ToHeldable() => null;

    public virtual Entity? Owner { get; protected set; }
    public Action<Entity> OnPickUp { get; protected init; }
    public Action OnThrow { get; protected init; } = () => { };
    public Func<(bool, IItem?)> OnUse { get; protected init; }
    public Action OnUnequip { get; protected init; } = () => { };

    protected Item(string name, char display, Tile? position = null)
    {
        Position = position;
        Name = name;
        Display = display;
        OnPickUp = entity => Owner = entity;
        OnUse = () => (false, this);
    }
}