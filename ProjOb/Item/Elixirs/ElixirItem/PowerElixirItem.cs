namespace ProjOb;

public class PowerElixirItem : Item,  IElixirItem
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
