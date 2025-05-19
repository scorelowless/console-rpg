namespace ProjOb.ActionType;

public class HandsFull : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    
    public bool IsSenderDead => false;
    public string Message => "Cannot equip because hands are taken!";
    public Entity Sender { get; }
    public HandsFull(Entity sender)
    {
        Sender = sender;
    }
}