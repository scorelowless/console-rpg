namespace ProjOb;

public abstract class Item : IItem
{
    public ColoredChar Display { get; set; }
    public virtual string Name { get; set; } = null!;
    public virtual string Info => "";
    public IItem ToItem() => this;
    public virtual IHeldable? ToHeldable() => null;

    public virtual Entity? Owner { get; set; }

    public virtual void OnPickUp(Entity entity) => Owner = entity;

    public virtual void OnThrow()
    {
        
    }

    public virtual (bool, IItem?) OnUse() => (false, this);

    public virtual void OnUnequip()
    {
        
    }

    protected Item(string name, char display, ConsoleColor color = ConsoleColor.White)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Name = name;
        Display = new ColoredChar(display, color);
    }

    public Item()
    {
    }
}