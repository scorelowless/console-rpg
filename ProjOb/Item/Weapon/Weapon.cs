namespace ProjOb;

public class Weapon : Item, IHeldable
{
    public Player? Owner { get; set; }
    public bool IsHeld { get; set; }
    
    public virtual int Damage { get; init; }
    public void Equip(Entity e)
    {
        throw new NotImplementedException();
    }

    public void Unequip(Entity e)
    {
        throw new NotImplementedException();
    }
}