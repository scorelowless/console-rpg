namespace ProjOb;

public class AdvancedKeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private readonly ConsoleKeyInfo _key;
    private readonly IActionType _action;

    public IActionType Check(ConsoleKeyInfo key)
    {
        if (key.Key == _key.Key && key.Modifiers == _key.Modifiers) return _action;
        return _baseKeyControl.Check(key);
    }

    public AdvancedKeyControl(IKeyControl baseKeyControl, ConsoleKeyInfo key, IActionType action)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
    }
}