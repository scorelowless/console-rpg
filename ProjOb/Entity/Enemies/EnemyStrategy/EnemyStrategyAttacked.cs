namespace ProjOb;

public class EnemyStrategyAttacked : IEnemyStrategy
{
    public IResultType? Execute(Enemy enemy, Player player)
    {
        int distance = enemy.Position.Map.CheckDistance(enemy.Position, player.Position);
        if (distance > 1) return null;
        return enemy.Attack(player);
    }
}