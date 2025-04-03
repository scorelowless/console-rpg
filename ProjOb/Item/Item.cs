namespace ProjOb;

public abstract class Item : IItem
{
    public char Display { get; }
    public virtual string Name { get; }
    public virtual string Info => "";
    public IItem ToItem() => this;
    public virtual IHeldable? ToHeldable() => null;

    public virtual Entity? Owner { get; protected set; }

    public virtual void OnPickUp(Entity entity)
    {
        Owner = entity;
    }

    public virtual void OnThrow()
    {
        
    }

    public virtual (bool, IItem?) OnUse() => (false, this);

    public virtual void OnUnequip()
    {
        
    }

    protected Item(string name, char display)
    {
        Name = name;
        Display = display;
    }
}