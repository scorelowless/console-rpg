namespace ProjOb;

public class AdvancedKeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private readonly ConsoleKeyInfo _key;
    private readonly Func<IActionType> _action;

    public IActionType Check(ConsoleKeyInfo key, ref bool isAwaitingInput)
    {
        if (!isAwaitingInput && key.Key == _key.Key && key.Modifiers == _key.Modifiers) return _action();
        return _baseKeyControl.Check(key, ref isAwaitingInput);

    }

    public AdvancedKeyControl(IKeyControl baseKeyControl, ConsoleKeyInfo key,  Func<IActionType> action)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
    }
}