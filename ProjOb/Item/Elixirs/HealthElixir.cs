namespace ProjOb.Elixirs;

public class HealthElixir : Item, IElixir
{
    private const int Value = 5;
    public HealthElixir() : base("Health elixir", 'E')
    {
        OnUse = () =>
        {
            Owner!.Stats["Health"] += Value;
            return (true, null);
        };
    }
}