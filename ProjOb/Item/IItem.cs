namespace ProjOb;

public interface IItem : IMappable
{
    public string Info { get; }
    public Entity? Owner { get; }
    public void OnPickUp(Entity entity);
    public void OnThrow();
    public (bool, IItem?) OnUse(); // returns if the item could be used/equipped and the item after use
    public void OnUnequip();
    public IHeldable? ToHeldable();
}