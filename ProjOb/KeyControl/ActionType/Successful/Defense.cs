namespace ProjOb.ActionType;

public class Defense : IActionType
{
    private readonly Entity _attacker;
    public Defense(Entity sender, Entity attacker)
    {
        Sender = sender;
        _attacker = attacker;
    }
    public bool WasSuccessful => true;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => $"Player attacked {_attacker.Name}";
    public Entity Sender { get; }
}