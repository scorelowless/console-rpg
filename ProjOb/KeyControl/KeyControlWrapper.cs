namespace ProjOb;

public class KeyControlWrapper : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    
    public IActionType Check(ConsoleKeyInfo key, ref bool isAwaitingInput)
    {
        return _baseKeyControl.Check(key, ref isAwaitingInput);
    }

    public KeyControlWrapper(IKeyControl baseKeyControl)
    {
        _baseKeyControl = baseKeyControl;
    }
}