using System.Text.Json.Serialization;

namespace ProjOb;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(EnemyStrategyAfraid), "EnemyStrategyAfraid")]
[JsonDerivedType(typeof(EnemyStrategyAggressive), "EnemyStrategyAggressive")]
[JsonDerivedType(typeof(EnemyStrategyCalm), "EnemyStrategyCalm")]
[JsonDerivedType(typeof(EnemyStrategyAttacked), "EnemyStrategyAttacked")]
public interface IEnemyStrategy
{
    IResultType? Execute(Enemy enemy, Player player);
}