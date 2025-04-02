namespace ProjOb;

public class KeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private readonly ConsoleKey _key;
    private readonly Action _action;

    public void Check(ConsoleKey key)
    {
        if (key == _key)
        {
            _action();
        }
        else
        {
            _baseKeyControl.Check(key);
        }
    }

    public KeyControl(IKeyControl baseKeyControl, ConsoleKey key,  Action action)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
    }
}