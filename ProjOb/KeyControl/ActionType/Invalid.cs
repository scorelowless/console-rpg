namespace ProjOb.ActionType;

public class Invalid : IActionType
{
    public int PlayerIndex { get; set; }
    public IResultType Execute(Model model)
    {
        return ResultType.Unsuccessful.GuardKeyControl();
    }
}