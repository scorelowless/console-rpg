namespace ProjOb.ActionType;

public class Move : IActionType
{
    public Direction Direction { get; set; } = Direction.Up;
    public int PlayerIndex { get; set; }
    public IResultType Execute(Model model)
    {
        return model.Players[PlayerIndex].Move(Direction);
    }

    private Move(int playerIndex, Direction direction)
    {
        PlayerIndex = playerIndex;
        Direction = direction;
    }
    
    public Move()
    {
    }
    
    public static Move Up(int playerIndex) => new(playerIndex, Direction.Up);
    public static Move Left(int playerIndex) => new(playerIndex, Direction.Left);
    public static Move Down(int playerIndex) => new(playerIndex, Direction.Down);
    public static Move Right(int playerIndex) => new(playerIndex, Direction.Right);
}