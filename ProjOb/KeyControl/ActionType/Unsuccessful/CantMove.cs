namespace ProjOb.ActionType;

public class CantMove : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => "Cannot move there!";
    public Entity Sender { get; }
    public CantMove(Entity sender)
    {
        Sender = sender;
    }
}