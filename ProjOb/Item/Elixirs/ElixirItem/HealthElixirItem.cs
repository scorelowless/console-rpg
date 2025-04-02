namespace ProjOb.Elixirs;

public class HealthElixirItem : Item, IElixirItem
{
    private const int Value = 5;
    public HealthElixirItem() : base("Health elixir", 'E')
    {
        OnUse = () =>
        {
            Owner!.Stats[Entity.StatsType.Health] += Value;
            return (true, null);
        };
    }
}