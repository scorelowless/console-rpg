namespace ProjOb;

public interface IItem : IMappable
{
    public string Info { get; }
    public Entity? Owner { get; }
    public Action<Entity> OnPickUp { get; }
    public Action OnThrow { get; }
    public Func<(bool, IItem?)> OnUse { get; } // returns if the item could be used/equipped and the item after use
    public Action OnUnequip { get; }
    public IHeldable? ToHeldable();
}