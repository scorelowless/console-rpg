namespace ProjOb;

public class AgilityElixirItem : Item, IElixirItem
{
    public AgilityElixirItem() : base("Agility elixir", 'E')
    {

    }
    public override (bool, IItem?) OnUse()
    {
        _ = new AgilityElixirEffect(Owner!);
        return (true, null);
    }
}