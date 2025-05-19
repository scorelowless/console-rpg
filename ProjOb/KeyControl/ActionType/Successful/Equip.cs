namespace ProjOb.ActionType;

public class Equip : IActionType
{
    private readonly IItem _item;
    public Equip(Entity sender, IItem item)
    {
        Sender = sender;
        _item = item;
    }
    public bool WasSuccessful => true;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => $"Player equipped {_item.Name}";
    public Entity Sender { get; }
}