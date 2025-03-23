using System.Collections.Immutable;
using ProjOb.Currencies;

namespace ProjOb;

public abstract class Entity : IMappable
{
    public string Name { get; }
    public IItem? ToItem() => null;
    private Tile _position;
    private readonly IHeldable?[] _heldItems;
    protected IItem?[] _inventory;
    
    public ICurrency[] Currencies { get; }

    public Tile? Position // TODO: fix nullability issue
    {
        get => _position;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _position = value;
        }
    }

    public char Display { get; init; }
    public Dictionary<string, int> Stats { get; }

    public ImmutableArray<IHeldable?> HeldItems => [.._heldItems];

    private bool[] IsHandTaken { get; }

    public ImmutableArray<IItem?> Inventory => [.._inventory];

    protected Entity(string name, Tile position)
    {
        Name = name;
        _position = position;
        position.Add(this);
        IsHandTaken = [false, false];
        _heldItems = [null, null];
        _inventory = [];
        Currencies = [new Money(0, 0), new  Gold(0, 1)];
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

    protected bool Grab(IHeldable heldable)
    {
        if (IsHandTaken[0] && IsHandTaken[1]) return false;
        if ((IsHandTaken[0] || IsHandTaken[1]) && heldable.HandsTaken == 2) return false;
        if (heldable.HandsTaken == 2)
        {
            IsHandTaken[0] = true;
            IsHandTaken[1] = true;
            _heldItems[0] =  heldable;
            _heldItems[1] =  heldable;
        }
        else
        {
            if (IsHandTaken[0])
            {
                IsHandTaken[1] = true;
                _heldItems[1] = heldable;
            }
            else
            {
                IsHandTaken[0] = true;
                _heldItems[0] = heldable;
            }
        }
        return true;
    }

    protected IHeldable? Ungrab()
    {
        var ret = HeldItems[1] != null ?  HeldItems[1] : HeldItems[0];
        if (HeldItems[0] == ret)
        {
            _heldItems[0] = null;
            IsHandTaken[0] = false;
        }
        if (HeldItems[1] == ret)
        {
            _heldItems[1] = null;
            IsHandTaken[1] = false;
        }
        return ret;
    }
    
}