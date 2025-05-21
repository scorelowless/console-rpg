namespace ProjOb.ActionType;

public class PickUp : IIndexedActionType
{
    public int PlayerIndex { get; set; }
    public int Ind { get; set; }
    public IResultType Execute(Model model)
    {
        return model.Players[PlayerIndex].PickUp(Ind);
    }
    public PickUp(int playerIndex)
    {
        PlayerIndex = playerIndex;
    }
    
    public PickUp()
    {
    }
}