namespace ProjOb.ActionType;

public class WrongAttack : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    
    public bool IsSenderDead => false;
    public string Message => "Invalid attack type!";
    public Entity Sender { get; }
    public WrongAttack(Entity sender)
    {
        Sender = sender;
    }
}