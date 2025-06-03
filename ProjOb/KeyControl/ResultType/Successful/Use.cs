namespace ProjOb.ResultType;

public class Use : IResultType
{
    private readonly IItem _item;
    public Use(IItem item)
    {
        _item = item;
    }
    public string Message => $"Player used {_item.Name}";
}