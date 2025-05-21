namespace ProjOb;

public class GuardKeyControl : IKeyControl
{
    public IActionType Check(ConsoleKeyInfo key)
    {
        return new ActionType.Invalid();
    }
}