using System.Collections.Immutable;

namespace ProjOb;

public abstract class Entity : IMappable
{
    public string Name { get; }
    public Tile Position { get; protected set; }
    private readonly IHeldable?[] _heldItems;

    public ColoredChar Display { get; }
    public Dictionary<StatsType, int> Stats { get; protected init; }

    public ImmutableArray<IHeldable?> HeldItems => [.._heldItems];

    private bool[] IsHandTaken { get; }
    
    private readonly List<IEffect> _effects = [];
    public ImmutableList<IEffect> Effects => _effects.ToImmutableList();
    private readonly List<ITourWatch?> _tourSubscribers = [];

    protected Entity(string name, char display, Tile position, ConsoleColor color)
    {
        Name = name;
        Display = new ColoredChar(display, color);
        Position = position;
        IsHandTaken = [false, false];
        _heldItems = [null, null];
        Stats = [];
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
    protected void NextTour()
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        for (int i = 0; i < _tourSubscribers.Count; i++)
        {
            _tourSubscribers[i]?.Update();
        }
        _tourSubscribers.RemoveAll(t => t == null);
    }
    public void AddSubscriber(ITourWatch watcher) => _tourSubscribers.Add(watcher);

    public void RemoveSubscriber(ITourWatch watcher)
    {
        int ind = _tourSubscribers.IndexOf(watcher);
        if (ind != -1)
        {
            _tourSubscribers[ind] = null;
        }
    }

    public void AddEffect(IEffect effect)
    {
        _effects.Add(effect);
        AddSubscriber(effect);
    }
    public void RemoveEffect(IEffect effect)
    {
        effect.OnRemove();
        _effects.Remove(effect);
        RemoveSubscriber(effect);
    }

    public enum StatsType
    {
        Power,
        Agility,
        Health,
        Luck,
        Aggression,
        Wisdom,
        Attack,
        Armor
    }
}