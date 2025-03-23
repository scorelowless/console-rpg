namespace ProjOb;

public interface IHeldable : IItem
{
    public int HandsTaken { get; }
    bool IsHeld { get; set; }
}