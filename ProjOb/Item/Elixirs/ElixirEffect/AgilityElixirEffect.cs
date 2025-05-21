namespace ProjOb;

public class AgilityElixirEffect : IEffect
{
    private readonly Entity _owner;
    public int ToursLeft { get; private set; } = 10 + 1;
    private const int VALUE = 5;
    public string Name => "Agility boost";

    public AgilityElixirEffect(Entity entity)
    {
        _owner = entity;
        entity.AddEffect(this);
        entity.Stats[Entity.StatsType.Agility] += VALUE;
    }
    public void Update()
    {
        ToursLeft--;
        if (ToursLeft > 0) return;
        _owner.RemoveEffect(this);
    }

    public void OnRemove()
    {
        _owner.Stats[Entity.StatsType.Agility] -= VALUE;
    }
}