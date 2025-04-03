using System.Collections.Immutable;

namespace ProjOb;

public class LastAction
{
    private readonly IItem? _involvedItem;
    private ActionType Action { get; }
    private readonly Enemy? _encounteredEnemy;
    private static readonly ImmutableDictionary<ActionType, bool> RequiresItem =
        ImmutableDictionary.Create<ActionType, bool>()
            .Add(ActionType.None, false)
            .Add(ActionType.MoveUp, false)
            .Add(ActionType.MoveRight, false)
            .Add(ActionType.MoveDown, false)
            .Add(ActionType.MoveLeft, false)
            .Add(ActionType.PickUp, true)
            .Add(ActionType.ThrowAway, true)
            .Add(ActionType.Use, true)
            .Add(ActionType.Equip, true)
            .Add(ActionType.Unequip, true)
            .Add(ActionType.DropEverything, false);

    public LastAction(ActionType action, IItem? involvedItem = null, Enemy? encounteredEnemy = null)
    {
        Action = action;
        _involvedItem = involvedItem;
        _encounteredEnemy = encounteredEnemy;
        if(RequiresItem[action] && _involvedItem == null) throw new InvalidOperationException("Action requires item");
    }
    
    public override string ToString()
    {
        string ret = Action switch
        {
            ActionType.MoveUp => "Player moved up",
            ActionType.MoveRight => "Player moved right",
            ActionType.MoveDown => "Player moved down",
            ActionType.MoveLeft => "Player moved left",
            ActionType.PickUp => $"Picked up {_involvedItem!.Name}",
            ActionType.ThrowAway => $"Player threw {_involvedItem!.Name} away",
            ActionType.Use => $"Player used {_involvedItem!.Name}",
            ActionType.Equip => $"Player equipped {_involvedItem!.Name}",
            ActionType.Unequip => $"Player unequipped {_involvedItem!.Name}",
            ActionType.DropEverything => "Player dropped entire inventory",
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
        DropEverything
    }
}