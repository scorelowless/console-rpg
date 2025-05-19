namespace ProjOb.ActionType;

public class Attack : IActionType
{
    private readonly Entity _target;
    public Attack(Entity sender, Entity target)
    {
        Sender = sender;
        _target = target;
    }
    public bool WasSuccessful => true;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message => $"Player attacked {_target.Name}";
    public Entity Sender { get; }
}