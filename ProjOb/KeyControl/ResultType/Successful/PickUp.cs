namespace ProjOb.ResultType;

public class PickUp : IResultType
{
    private readonly IItem _item;
    public PickUp(IItem item)
    {
        _item = item;
    }
    public bool WasSuccessful => true;
    public bool IsSenderDead => false;
    public string Message => $"Picked up {_item.Name}";
}