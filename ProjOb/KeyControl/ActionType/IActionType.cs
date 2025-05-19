namespace ProjOb;

public interface IActionType
{
    bool WasSuccessful { get; }
    bool IsPrompt { get; }
    bool IsSenderDead { get; }
    string Message { get; }
    Entity Sender { get; }
}