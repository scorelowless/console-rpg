namespace ProjOb.ActionType;

public class NoEnemy : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    
    public bool IsSenderDead => false;
    public string Message => "No enemy to attack!";
    public Entity Sender { get; }
    public NoEnemy(Entity sender)
    {
        Sender = sender;
    }
}