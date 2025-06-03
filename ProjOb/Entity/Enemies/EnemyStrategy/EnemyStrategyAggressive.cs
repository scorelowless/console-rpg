using System.Drawing;
using ProjOb.ResultType;

namespace ProjOb;

public class EnemyStrategyAggressive : IEnemyStrategy
{
    public IResultType? Execute(Enemy enemy, Player player)
    {
        int distance = enemy.Position.Map.CheckDistance(enemy.Position, player.Position);
        if (distance > 5) return null;
        Point e = enemy.Position.Position;
        Point p = player.Position.Position;
        if (int.Abs(e.X - p.X) + int.Abs(e.Y - p.Y) == 1)
        {
            return enemy.Attack(player);
        }
        if(e.X < p.X)
        {
            if (enemy.Move(Direction.Right).WasSuccessful) return new EnemyMove(enemy);
        }
        if(e.X > p.X)
        {
            if (enemy.Move(Direction.Left).WasSuccessful) return new EnemyMove(enemy);
        }
        if(e.Y < p.Y)
        {
            if(enemy.Move(Direction.Down).WasSuccessful) return new EnemyMove(enemy);
        }
        if(e.Y > p.Y)
        {
            if(enemy.Move(Direction.Up).WasSuccessful) return new EnemyMove(enemy);
        }
        return null;
    }
}