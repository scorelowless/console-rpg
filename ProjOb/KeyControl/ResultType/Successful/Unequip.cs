namespace ProjOb.ResultType;

public class Unequip : IResultType
{
    private readonly IItem _item;
    public Unequip(IItem item)
    {
        _item = item;
    }
    public string Message => $"Player unequipped {_item.Name}";
}