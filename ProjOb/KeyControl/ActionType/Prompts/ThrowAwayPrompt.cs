namespace ProjOb.ActionType;

public class ThrowAwayPrompt : IActionType
{
    public bool WasSuccessful => true;
    public bool IsPrompt => true;
    public bool IsSenderDead => false;
    public string Message => "Which item from the inventory you want to throw away (number or letter)";
    public Entity Sender { get; }
    public ThrowAwayPrompt(Entity sender)
    {
        Sender = sender;
    }
}