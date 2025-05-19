namespace ProjOb;

public class AdvancedKeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private readonly ConsoleKeyInfo _key;
    private readonly Func<int> _action;

    public int Check(ConsoleKeyInfo key)
    {
        if (key.Key != _key.Key || key.Modifiers != _key.Modifiers) return _baseKeyControl.Check(key);
        return _action();

    }

    public AdvancedKeyControl(IKeyControl baseKeyControl, ConsoleKeyInfo key,  Func<int> action)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
    }
}