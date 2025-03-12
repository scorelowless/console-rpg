using System.Drawing;

namespace ProjOb;

public class Entity : IMappable
{
    public Point Position { get; set; }
    public char Display { get; set; }
    public Map Map { get; set; }
    public Attributes Stats { get; set; } = new();
    public List<IHeldable> HeldItems { get; set; } = [];
    private int _numberOfHands;
    private int _usedHands;

    public Item[] Inventory { get; set; } = [];

    public Entity(Map map)
    {
        Map = map;
        _numberOfHands = 2;
        _usedHands = 0;
    }

    public void Grab(IHeldable heldable)
    {
        if (_usedHands + heldable.HandsTaken >= _numberOfHands)
            return;
        _usedHands += heldable.HandsTaken;
        HeldItems.Add(heldable);
        heldable.OnGrab();
        heldable.IsHeld = true;
    }

    public void Ungrab(IHeldable heldable)
    {
        if (!HeldItems.Contains(heldable))
            return;
        HeldItems.Remove(heldable);
        heldable.OnUngrab();
        heldable.IsHeld = false;
    }
    
}