namespace ProjOb.Elixirs;

public class StrengthElixir : Item, IElixir
{
    private const int Value = 5;
    public StrengthElixir() : base("Strength elixir", 'E')
    {
        OnUse = () =>
        {
            Owner!.Stats["Strength"] += Value;
            return (true, null);
        };
    }
}