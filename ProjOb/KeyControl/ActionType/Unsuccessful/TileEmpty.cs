namespace ProjOb.ActionType;

public class TileEmpty : IActionType
{
    public bool WasSuccessful => false;
    public bool IsPrompt => false;
    
    public bool IsSenderDead => false;
    public string Message => "Tile is empty!";
    public Entity Sender { get; }
    public TileEmpty(Entity sender)
    {
        Sender = sender;
    }
}