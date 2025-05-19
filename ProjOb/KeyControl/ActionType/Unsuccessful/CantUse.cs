namespace ProjOb.ActionType;

public class CantUse : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => "Cannot use this item!";
    public Entity Sender { get; }
    public CantUse(Entity sender)
    {
        Sender = sender;
    }
}