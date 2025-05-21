namespace ProjOb.ActionType;

public class Die : IActionType
{
    public int PlayerIndex { get; set; }
    public IResultType Execute(Model model)
    {
        return model.Players[PlayerIndex].Die();
    }

    public Die(int playerIndex)
    {
        PlayerIndex = playerIndex;
    }
}