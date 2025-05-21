namespace ProjOb.ResultType;

public class Equip : IResultType
{
    private readonly IItem _item;
    public Equip(IItem item)
    {
        _item = item;
    }
    public bool WasSuccessful => true;
    public bool IsSenderDead => false;
    public string Message => $"Player equipped {_item.Name}";
}