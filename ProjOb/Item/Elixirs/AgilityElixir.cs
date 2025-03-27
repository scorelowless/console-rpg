namespace ProjOb.Elixirs;

public class AgilityElixir : Item, IElixir
{
    private const int Value = 5;
    public AgilityElixir() : base("Agility elixir", 'E')
    {
        OnUse = () =>
        {
            Owner!.Stats[Entity.StatsType.Agility] += Value;
            return (true, null);
        };
    }
}