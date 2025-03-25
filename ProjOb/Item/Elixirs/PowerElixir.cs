namespace ProjOb.Elixirs;

public class PowerElixir : Item,  IElixir
{
    protected static int Value = 5;

    public PowerElixir() : base("Power elixir", 'E')
    {
        OnUse = () =>
        {
            Owner!.Stats["Power"] += Value;
            return (true, null);
        };
    }
}