namespace ProjOb.Elixirs;

public class PowerElixir : Item,  IElixir
{
    protected static int Value = 5; // remove static bc making any StrongPowerElixir makes all PowerElixirs add 10 

    public PowerElixir() : base("Power elixir", 'E')
    {
        OnUse = () =>
        {
            Owner!.Stats[Entity.StatsType.Power] += Value;
            return (true, null);
        };
    }
}
