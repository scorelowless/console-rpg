namespace ProjOb.ResultType;

public class Move : IResultType
{
    private readonly Direction _direction;
    private readonly Enemy? _nearbyEnemy;
    public Move(Direction direction, Enemy? nearbyEnemy)
    {
        _direction = direction;
        _nearbyEnemy = nearbyEnemy;
    }
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
}