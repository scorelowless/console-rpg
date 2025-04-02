namespace ProjOb.Tours.ElixirEffect;

public class AgilityElixirEffect : IEffect
{
    private int _toursLeft = 10;
    private const int Value = 5;
    private readonly Entity _owner;

    public AgilityElixirEffect(Entity entity)
    {
        _owner = entity;
        entity.AddEffect(this);
        entity.Stats[Entity.StatsType.Agility] += Value;
    }
    public void Update()
    {
        _toursLeft--;
        if (_toursLeft > 0) return;
        _owner.RemoveEffect(this);
        _owner.Stats[Entity.StatsType.Agility] -= Value;
    }
}