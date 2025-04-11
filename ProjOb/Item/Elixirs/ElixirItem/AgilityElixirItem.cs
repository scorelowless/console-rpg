namespace ProjOb;

public class AgilityElixirItem : ElixirItem
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