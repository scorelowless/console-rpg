namespace ProjOb.Tours.ElixirEffect;

public class PowerElixirEffect : IEffect
{
    private int _toursLeft = 10;
    private readonly Entity _owner;

    public PowerElixirEffect(Entity entity)
    {
        _owner = entity;
        entity.AddEffect(this);
        entity.Stats[Entity.StatsType.Power] += _toursLeft;
    }
    public void Update()
    {
        _toursLeft--;
        _owner.Stats[Entity.StatsType.Power] -= 1;
        if (_toursLeft > 0) return;
        _owner.RemoveEffect(this);
    }
}