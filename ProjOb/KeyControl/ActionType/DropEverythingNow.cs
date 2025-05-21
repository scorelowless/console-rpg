namespace ProjOb.ActionType;

public class DropEverythingNow : IActionType
{
    public int PlayerIndex { get; set; }
    public IResultType Execute(Model model)
    {
        return model.Players[PlayerIndex].DropEverythingNow();
    }
    public DropEverythingNow(int playerIndex)
    {
        PlayerIndex = playerIndex;
    }
    
    public DropEverythingNow()
    {
    }
}