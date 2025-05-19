namespace ProjOb.ActionType;

public class DropEverything : IActionType
{
    public bool WasSuccessful => true;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => "Player dropped entire inventory";
    public Entity Sender { get; }
    public DropEverything(Entity sender)
    {
        Sender = sender;
    }
}