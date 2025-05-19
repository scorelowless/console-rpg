namespace ProjOb;

public class KeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private readonly ConsoleKey _key;
    private readonly Func<IActionType> _action;

    public IActionType Check(ConsoleKeyInfo key, ref bool isAwaitingInput)
    {
        if (!isAwaitingInput && key.Key == _key) return _action();
        return _baseKeyControl.Check(key, ref isAwaitingInput);
    }

    public KeyControl(IKeyControl baseKeyControl, ConsoleKey key,  Func<IActionType> action)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
    }
}