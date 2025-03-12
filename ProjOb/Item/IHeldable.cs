namespace ProjOb;

public interface IHeldable
{
    public int HandsTaken { get; }
    Entity? Owner { get; }
    bool IsHeld { get; set; }
    Action OnGrab { get; }
    Action OnUngrab { get; }
}