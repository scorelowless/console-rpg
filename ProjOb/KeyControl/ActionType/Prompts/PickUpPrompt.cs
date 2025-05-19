namespace ProjOb.ActionType;

public class PickUpPrompt : IActionType
{
    public bool WasSuccessful => true;
    public bool IsPrompt => true;
    public bool IsSenderDead => false;
    public string Message => "Which item from the tile contents you want to pick up (number or letter)";
    public Entity Sender { get; }
    public PickUpPrompt(Entity sender)
    {
        Sender = sender;
    }
}