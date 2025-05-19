namespace ProjOb.ActionType;

public class NothingHeld : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    
    public bool IsSenderDead => false;
    public string Message => "Nothing is held!";
    public Entity Sender { get; }
    public NothingHeld(Entity sender)
    {
        Sender = sender;
    }
}