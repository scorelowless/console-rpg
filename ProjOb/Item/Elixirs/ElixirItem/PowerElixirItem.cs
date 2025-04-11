namespace ProjOb;

public class PowerElixirItem : ElixirItem
{
    public PowerElixirItem() : base("Power elixir", 'E')
    {

    }
    public override (bool, IItem?) OnUse()
    {
        _ = new PowerElixirEffect(Owner!);
        return (true, null);
    }
}
