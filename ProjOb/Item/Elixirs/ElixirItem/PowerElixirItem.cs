namespace ProjOb.Elixirs;

public class PowerElixirItem : Item,  IElixirItem
{
    private const int Value = 5;

    public PowerElixirItem() : base("Power elixir", 'E')
    {
        OnUse = () =>
        {
            _ = new Tours.ElixirEffect.PowerElixirEffect(Owner);
            return (true, null);
        };
    }
}
