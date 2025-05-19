namespace ProjOb.ActionType;

public class Death : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    public bool IsSenderDead => true;
    public string Message => "You are dead!";
    public Entity Sender { get; }
    public Death(Entity sender)
    {
        Sender = sender;
    }
}