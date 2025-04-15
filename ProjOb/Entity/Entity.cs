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
    
    private readonly List<IEffect> _effects = [];
    public ImmutableList<IEffect> Effects => _effects.ToImmutableList();
    private readonly List<ITourWatch?> _tourSubscribers = [];

    protected Entity(string name, char display, Tile position, ConsoleColor color)
    {
        Name = name;
        Display = new ColoredChar(display, color);
        Position = position;
        _heldItems = [null, null];
        Stats = [];
    }

    protected bool Grab(IHeldable heldable)
    {
        switch (L: _heldItems[0] != null, R: _heldItems[1] != null, Two: heldable.HandsTaken == 2)
        {
            case (true, true, _): // both hands taken
            case (true, false, true): // right hand taken and heldable is two-handed
            case (false, true, true): // left hand taken and heldable is two-handed
                return false;
            case(false, false, true): // two hands free and heldable is two-handed
                _heldItems[0] = heldable;
                _heldItems[1] = heldable;
                return true;
            case (false, _, false): // left hand free and heldable is one-handed
                _heldItems[0] = heldable;
                return true;
            case(true, false, false): // right hand free and heldable is one-handed
                _heldItems[1] = heldable;
                return true;
        }
    }

    protected IHeldable? Ungrab()
    {
        var ret = _heldItems[1] != null ? _heldItems[1] : _heldItems[0];
        if (_heldItems[0] == ret)
        {
            _heldItems[0] = null;
        }
        if (_heldItems[1] == ret)
        {
            _heldItems[1] = null;
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