namespace ProjOb.ActionType;

public class Unequip : IActionType
{
    public int PlayerIndex { get; set; }
    public IResultType Execute(Model model)
    {
        return model.Players[PlayerIndex].Unequip();
    }
    public Unequip(int playerIndex)
    {
        PlayerIndex = playerIndex;
    }
    
    public Unequip()
    {
    }
}