namespace ProjOb.ResultType;

public class EnemyMove : IResultType
{
    private readonly Enemy _enemy;
    public EnemyMove(Enemy enemy)
    {
        _enemy = enemy;
    }
    public string Message => $"{_enemy.Name} moved towards the player";
}