namespace ProjOb;

public abstract class Entity : IMappable
{
    public string Name { get; set; } = null!;
    public Tile Position { get; set; } = null!;
    
    public Inventory Inventory { get; set; } = new();

    public ColoredChar Display { get; set; }
    public Dictionary<StatsType, int> Stats { get; set; } = null!;
    public List<IEffect> Effects { get; set; } = [];
    public List<ITourWatch?> TourSubscribers { get; set; } = [];

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

    public Entity()
    {
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
        for (int i = 0; i < TourSubscribers.Count; i++)
        {
            TourSubscribers[i]?.Update();
        }
        TourSubscribers.RemoveAll(t => t == null);
    }
    public void AddSubscriber(ITourWatch watcher) => TourSubscribers.Add(watcher);

    public void RemoveSubscriber(ITourWatch watcher)
    {
        int ind = TourSubscribers.IndexOf(watcher);
        if (ind != -1)
        {
            TourSubscribers[ind] = null;
        }
    }

    public void AddEffect(IEffect effect)
    {
        Effects.Add(effect);
        AddSubscriber(effect);
    }
    public void RemoveEffect(IEffect effect)
    {
        effect.OnRemove();
        Effects.Remove(effect);
        RemoveSubscriber(effect);
    }

    public abstract void ReceiveDamage(int damage);

    protected abstract IResultType Attack(int type, Entity target);

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