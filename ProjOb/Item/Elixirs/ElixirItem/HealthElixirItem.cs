namespace ProjOb;

public class HealthElixirItem : ElixirItem
{
    private const int VALUE = 5;
    public HealthElixirItem() : base("Health elixir", 'E')
    {

    }
    
    public override (bool, IItem?) OnUse()
    {
        Owner!.Stats[Entity.StatsType.Health] += VALUE;
        return (true, null);
    }
}