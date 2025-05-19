namespace ProjOb.ActionType;

public class GuardKeyControl : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    
    public bool IsSenderDead => false;
    public string Message => "This key does nothing!";
    public Entity Sender { get; }
    public GuardKeyControl(Entity sender)
    {
        Sender = sender;
    }
}