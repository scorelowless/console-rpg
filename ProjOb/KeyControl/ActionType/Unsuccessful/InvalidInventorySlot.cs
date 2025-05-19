namespace ProjOb.ActionType;

public class InvalidInventorySlot : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    
    public bool IsSenderDead => false;
    public string Message => "Invalid inventory slot!";
    public Entity Sender { get; }
    public InvalidInventorySlot(Entity sender)
    {
        Sender = sender;
    }
}