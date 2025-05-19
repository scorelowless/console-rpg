namespace ProjOb.ActionType;

public class Move : IActionType
{
    private readonly Direction _direction;
    private readonly Enemy? _nearbyEnemy;
    public Move(Entity sender, Direction direction, Enemy? nearbyEnemy)
    {
        Sender = sender;
        _direction = direction;
        _nearbyEnemy = nearbyEnemy;
    }
    public bool WasSuccessful => true;
    public bool IsPrompt => false;
    public bool IsSenderDead => false;
    public string Message
    {
        get
        {
            string ret = _direction switch
            {
                Direction.Up => "Player moved up",
                Direction.Right => "Player moved right",
                Direction.Down => "Player moved down",
                Direction.Left => "Player moved left",
                _ => throw new ArgumentOutOfRangeException(nameof(_direction), _direction, null)
            };
            if (_nearbyEnemy != null)
            {
                ret += $" and encountered {_nearbyEnemy.Name}";
            }

            return ret;
        }
    }
    public Entity Sender { get; }
}