namespace ProjOb;

public interface IHeldable
{
    Entity? Owner { get; }
    void Grab();
    void Ungrab();
}