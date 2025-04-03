namespace ProjOb;

public class PowerElixirEffect : IEffect
{
    private readonly Entity _owner;
    public int ToursLeft { get; private set; } = 10 + 1;
    public string Name => "Power boost";

    public PowerElixirEffect(Entity entity)
    {
        _owner = entity;
        entity.AddEffect(this);
        entity.Stats[Entity.StatsType.Power] += ToursLeft;
    }
    public void Update()
    {
        ToursLeft--;
        _owner.Stats[Entity.StatsType.Power] -= 1;
        if (ToursLeft > 0) return;
        _owner.RemoveEffect(this);
    }

    public void OnRemove()
    {
        _owner.Stats[Entity.StatsType.Power] -= ToursLeft;
    }
}