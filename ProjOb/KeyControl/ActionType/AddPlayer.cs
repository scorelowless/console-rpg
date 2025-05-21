namespace ProjOb.ActionType;

public class AddPlayer : IActionType
{
    public int PlayerIndex { get; set; }
    public IResultType Execute(Model model)
    {
        Player player = new Player(model.Map,  PlayerIndex);
        model.Players.Add(player);
        return new ResultType.PlayerAdded();
    }

    public AddPlayer(int playerIndex)
    {
        PlayerIndex = playerIndex;
    }

    public AddPlayer()
    {
    }
}