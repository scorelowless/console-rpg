namespace ProjOb.ActionType;

public class ThrowAway : IIndexedActionType
{
    public int PlayerIndex { get; set; }
    public int Ind { get; set; }
    public IResultType Execute(Model model)
    {
        return model.Players[PlayerIndex].ThrowAway(Ind);
    }
    public ThrowAway(int playerIndex)
    {
        PlayerIndex = playerIndex;
    }
    
    public ThrowAway()
    {
    }
}