namespace ProjOb.ActionType;

public class PickUp : IActionType
{
    private readonly IItem _item;
    public PickUp(Entity sender, IItem item)
    {
        Sender = sender;
        _item = item;
    }
    public bool WasSuccessful => true;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => $"Picked up {_item.Name}";
    public Entity Sender { get; }
}