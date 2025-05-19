using System.Collections.Immutable;

namespace ProjOb;

public abstract class Entity : IMappable
{
    public string Name { get; }
    public Tile Position { get; protected set; }
    
    public Inventory Inventory { get; } = new();

    public ColoredChar Display { get; }
    public Dictionary<StatsType, int> Stats { get; }
    
    private readonly List<IEffect> _effects = [];
    public ImmutableList<IEffect> Effects => _effects.ToImmutableList();
    private readonly List<ITourWatch?> _tourSubscribers = [];

    protected Entity(string name, char display, Tile position, ConsoleColor color)
    {
        Name = name;
        Display = new ColoredChar(display, color);
        Position = position;
        Stats = new Dictionary<StatsType, int>
        {
            { StatsType.Power, 0 },
            { StatsType.Agility, 0 },
            { StatsType.Luck, 0 },
            { StatsType.Aggression, 0 },
            { StatsType.Wisdom, 0 },
            { StatsType.Health, 0 },
            { StatsType.Armor, 0}
        };
    }

    protected void SetStats(int power, int agility, int luck, int aggression, int wisdom, int health, int armor)
    {
        Stats[StatsType.Power] = power;
        Stats[StatsType.Agility] = agility;
        Stats[StatsType.Luck] = luck;
        Stats[StatsType.Aggression] = aggression;
        Stats[StatsType.Wisdom] = wisdom;
        Stats[StatsType.Health] = health;
        Stats[StatsType.Armor] = armor;
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

    public abstract void ReceiveDamage(int damage);

    protected abstract IActionType Attack(int type, Entity target);

    public enum StatsType
    {
        Power,
        Agility,
        Health,
        Luck,
        Aggression,
        Wisdom,
        Armor
    }
}