namespace ProjOb.ActionType;

public class Use : IActionType
{
    private readonly IItem _item;
    public Use(Entity sender, IItem item)
    {
        Sender = sender;
        _item = item;
    }
    public bool WasSuccessful => true;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => $"Player used {_item.Name}";
    public Entity Sender { get; }
}