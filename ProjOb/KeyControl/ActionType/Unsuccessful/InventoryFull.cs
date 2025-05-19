namespace ProjOb.ActionType;

public class InventoryFull : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    
    public bool IsSenderDead => false;
    public string Message => "Inventory is full!";
    public Entity Sender { get; }
    public InventoryFull(Entity sender)
    {
        Sender = sender;
    }
}