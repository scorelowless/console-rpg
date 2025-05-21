namespace ProjOb.ActionType;

public class Use : IIndexedActionType
{
    public int PlayerIndex { get; set; }
    public int Ind { get; set; }
    public IResultType Execute(Model model)
    {
        return model.Players[PlayerIndex].Use(Ind);
    }
    public Use(int playerIndex)
    {
        PlayerIndex = playerIndex;
    }
    
    public Use()
    {
    }
}