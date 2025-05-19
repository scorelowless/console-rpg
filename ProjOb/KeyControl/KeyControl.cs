namespace ProjOb;

public class KeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private readonly ConsoleKey _key;
    private readonly Func<int> _action;

    public int Check(ConsoleKeyInfo key)
    {
        if (key.Key == _key) return _action();
        return _baseKeyControl.Check(key);
    }

    public KeyControl(IKeyControl baseKeyControl, ConsoleKey key,  Func<int> action)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
    }
}