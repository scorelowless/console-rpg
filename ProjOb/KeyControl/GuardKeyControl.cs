namespace ProjOb;

public class GuardKeyControl : IKeyControl
{
    private readonly Player _player;
    public IActionType Check(ConsoleKeyInfo key, ref bool isAwaitingInput)
    {
        return new ActionType.GuardKeyControl(_player);
    }

    public GuardKeyControl(Player player)
    {
        _player = player;
    }
}