namespace ProjOb;

public class HealthElixirItem : Item, IElixirItem
{
    private const int Value = 5;
    public HealthElixirItem() : base("Health elixir", 'E')
    {

    }
    
    public override (bool, IItem?) OnUse()
    {
        Owner!.Stats[Entity.StatsType.Health] += Value;
        return (true, null);
    }
}