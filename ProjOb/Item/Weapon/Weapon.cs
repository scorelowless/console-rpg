namespace ProjOb;

public class Weapon : Item, IHeldable
{
    public Entity? Owner { get; set; }
    public bool IsHeld { get; set; }
    
    public virtual int Damage { get; init; }
    public virtual void Grab()
    {
        throw new NotImplementedException();
    }

    public virtual void Ungrab()
    {
        throw new NotImplementedException();
    }
}