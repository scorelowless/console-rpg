namespace ProjOb.ActionType;

public class AttackTypePrompt : IActionType
{
    public bool WasSuccessful => true;
    public bool IsPrompt => true;
    public bool IsSenderDead => false;

    public string Message => """
                             What kind of attack you want to do?
                             1. Normal attack
                             2. Hidden attack
                             3. Magic attack
                             """;
    public Entity Sender { get; }
    public AttackTypePrompt(Entity sender)
    {
        Sender = sender;
    }
}