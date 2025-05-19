namespace ProjOb.ActionType;

public class Unequip : IActionType
{
    private readonly IItem _item;
    public Unequip(Entity sender, IItem item)
    {
        Sender = sender;
        _item = item;
    }
    public bool WasSuccessful => true;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => $"Player unequipped {_item.Name}";
    public Entity Sender { get; }
}