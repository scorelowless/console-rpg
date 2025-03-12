using System.Drawing;

namespace ProjOb;

public abstract class Entity : IMappable
{
    public string Name { get; set; }
    public Action<Entity> OnPickUp { get; } = _ => { };
    public Action<Entity> OnThrow { get; } = _ => { };
    public Tile Position { get; set; }
    public char Display { get; set; }
    public Dictionary<string, int> Stats { get; }
    
    public IWeapon?[] HeldItems { get; }
    public bool[] IsHandTaken { get; }

    public IMappable?[] Inventory { get; set; } = [];

    protected Entity(string name, Tile position)
    {
        Name = name;
        Position = position;
        position.Add(this);
        IsHandTaken = [false, false];
        HeldItems = [null, null];
        Stats = new Dictionary<string, int>
        {
            { "Power", 10 },
            { "Agility", 10 },
            { "Health", 10 },
            { "Luck", 10 },
            { "Aggression", 10 },
            { "Wisdom", 10 }
        };
    }

    protected bool Grab(IWeapon heldable)
    {
        if (IsHandTaken[0] && IsHandTaken[1]) return false;
        if ((IsHandTaken[0] || IsHandTaken[1]) && heldable.HandsTaken == 2) return false;
        if (heldable.HandsTaken == 2)
        {
            IsHandTaken[0] = true;
            IsHandTaken[1] = true;
            HeldItems[0] =  heldable;
            HeldItems[1] =  heldable;
        }
        else
        {
            if (IsHandTaken[0])
            {
                IsHandTaken[1] = true;
                HeldItems[1] = heldable;
            }
            else
            {
                IsHandTaken[0] = true;
                HeldItems[0] = heldable;
            }
        }
        heldable.OnGrab();
        return true;
    }

    public void Ungrab(IWeapon heldable)
    {
        if (!HeldItems.Contains(heldable))
            return;
        if (HeldItems[0] == heldable)
        {
            HeldItems[0] = null;
            IsHandTaken[0] = false;
        }
        if (HeldItems[1] == heldable)
        {
            HeldItems[1] = null;
            IsHandTaken[1] = false;
        }
        heldable.OnUngrab();
    }
    
}