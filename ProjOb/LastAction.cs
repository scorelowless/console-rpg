namespace ProjOb;

public class LastAction
{
    private readonly IItem? _involvedItem;
    private ActionType Action { get; }
    private readonly Enemy? _encounteredEnemy;
    private static readonly bool[] RequiresItem = [false, false, false, false, true, true, true, true];

    public LastAction(ActionType action, IItem? involvedItem = null, Enemy? encounteredEnemy = null)
    {
        Action = action;
        _involvedItem = involvedItem;
        _encounteredEnemy = encounteredEnemy;
        if(RequiresItem[(int)action]) throw new InvalidOperationException("Action requires item");
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
            ActionType.Use => $"Player used/equipped {_involvedItem!.Name}",
            ActionType.Unequip => $"Player unequipped {_involvedItem!.Name}",
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
        Unequip
    }
}