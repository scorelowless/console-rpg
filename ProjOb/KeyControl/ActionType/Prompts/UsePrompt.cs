namespace ProjOb.ActionType;

public class UsePrompt : IActionType
{
    public bool WasSuccessful => true;
    public bool IsPrompt => true;
    public bool IsSenderDead => false;
    public string Message => "Which item from the inventory you want to use/equip (number or letter)";
    public Entity Sender { get; }
    public UsePrompt(Entity sender)
    {
        Sender = sender;
    }
}