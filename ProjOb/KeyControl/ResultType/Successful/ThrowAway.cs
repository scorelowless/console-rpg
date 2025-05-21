namespace ProjOb.ResultType;

public class ThrowAway : IResultType
{
    private readonly IItem _item;
    public ThrowAway(IItem item)
    {
        _item = item;
    }
    public bool WasSuccessful => true;
    public bool IsSenderDead => false;
    public string Message => $"Player threw {_item.Name} away";
}