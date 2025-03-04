namespace ProjOb;

public interface IHeldable
{
    void Equip(Entity e);
    void Unequip(Entity e);
}