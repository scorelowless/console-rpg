namespace ProjOb;

public class AdvancedKeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private readonly ConsoleKeyInfo _key;
    private readonly Action _action;

    public void Check(ConsoleKeyInfo key)
    {
        if (key.Key == _key.Key && key.Modifiers == _key.Modifiers)
        {
            _action();
        }
        else
        {
            _baseKeyControl.Check(key);
        }
    }

    public AdvancedKeyControl(IKeyControl baseKeyControl, ConsoleKeyInfo key,  Action action)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
    }
}