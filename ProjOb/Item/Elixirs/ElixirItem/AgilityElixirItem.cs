using ProjOb.Tours.ElixirEffect;

namespace ProjOb.Elixirs;

public class AgilityElixirItem : Item, IElixirItem
{
    private const int Value = 5;
    public AgilityElixirItem() : base("Agility elixir", 'E')
    {
        OnUse = () =>
        {
            _ = new AgilityElixirEffect(Owner);
            return (true, null);
        };
    }
}