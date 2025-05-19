namespace ProjOb.ActionType;

public class ThrowAway : IActionType
{
    private readonly IItem _item;
    public ThrowAway(Entity sender, IItem item)
    {
        Sender = sender;
        _item = item;
    }
    public bool WasSuccessful => true;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => $"Player threw {_item.Name} away";
    public Entity Sender { get; }
}