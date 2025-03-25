namespace ProjOb.Elixirs;

public class AgilityElixir : Item, IElixir
{
    private const int Value = 5;
    public AgilityElixir() : base("Agility elixir", 'E')
    {
        OnUse = () =>
        {
            Owner!.Stats["Agility"] += Value;
            return (true, null);
        };
    }
}