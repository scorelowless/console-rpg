using System.Collections.Immutable;
using System.Security.AccessControl;

namespace ProjOb;

public class LastAction
{
    private ActionType Action { get; }
    private readonly string[] _parameters;
    private readonly Enemy? _encounteredEnemy;
    private static readonly ImmutableDictionary<ActionType, int> NumberOfParameters =
        ImmutableDictionary.Create<ActionType, int>()
            .Add(ActionType.None, 0)
            .Add(ActionType.MoveUp, 0)
            .Add(ActionType.MoveRight, 0)
            .Add(ActionType.MoveDown, 0)
            .Add(ActionType.MoveLeft, 0)
            .Add(ActionType.PickUp, 1)
            .Add(ActionType.ThrowAway, 1)
            .Add(ActionType.Use, 1)
            .Add(ActionType.Equip, 1)
            .Add(ActionType.Unequip, 1)
            .Add(ActionType.DropEverything, 0)
            .Add(ActionType.Attack, 1)
            .Add(ActionType.Defense, 1);

    public LastAction(ActionType action, Enemy? encounteredEnemy = null) : this(action, [], encounteredEnemy)
    {
        
    }
    public LastAction(ActionType action, string[] parameters, Enemy? encounteredEnemy = null)
    {
        Action = action;
        _parameters = parameters;
        if(NumberOfParameters[action] != parameters.Length)
            throw new InvalidOperationException("Wrong number of parameters in constructor of LastAction");
        _encounteredEnemy = encounteredEnemy;
    }
    
    public override string ToString()
    {
        string ret = Action switch
        {
            ActionType.MoveUp => "Player moved up",
            ActionType.MoveRight => "Player moved right",
            ActionType.MoveDown => "Player moved down",
            ActionType.MoveLeft => "Player moved left",
            ActionType.PickUp => $"Picked up {_parameters[0]}",
            ActionType.ThrowAway => $"Player threw {_parameters[0]} away",
            ActionType.Use => $"Player used {_parameters[0]}",
            ActionType.Equip => $"Player equipped {_parameters[0]}",
            ActionType.Unequip => $"Player unequipped {_parameters[0]}",
            ActionType.DropEverything => "Player dropped entire inventory",
            ActionType.Attack => $"Player attacked {_parameters[0]}",
            ActionType.Defense => $"Player got attacked by {_parameters[0]}",
            _ => ""
        };
        if (_encounteredEnemy != null) ret += $" and encountered {_encounteredEnemy.Name}";
        return ret;
    }

    public enum ActionType
    {
        None,
        MoveUp,
        MoveRight,
        MoveDown,
        MoveLeft,
        PickUp,
        ThrowAway,
        Use,
        Equip,
        Unequip,
        DropEverything,
        Attack,
        Defense
    }
}