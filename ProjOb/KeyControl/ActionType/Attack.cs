namespace ProjOb.ActionType;

public class Attack : IIndexedActionType
{
    public int PlayerIndex { get; set; }
    public int Ind { get; set; }
    public IResultType Execute(Model model)
    {
        return model.Players[PlayerIndex].Attack(Ind);
    }
    public Attack(int playerIndex)
    {
        PlayerIndex = playerIndex;
    }
    
    public Attack()
    {
    }
}