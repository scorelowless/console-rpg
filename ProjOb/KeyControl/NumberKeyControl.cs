using ProjOb.ActionType;

namespace ProjOb;

public class NumberKeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private const string NUMBERS = "123456789abcdefghijklmnopqrstuvwxyz";
    private readonly ConsoleKey _key;
    private readonly Func<int, IActionType> _action;
    private readonly IActionType _promptAction;
    private bool _isAwaitingInput;
    
    public IActionType Check(ConsoleKeyInfo key, ref bool isAwaitingInput)
    {
        if(_isAwaitingInput && !isAwaitingInput) throw new Exception("Unexpected state in NumberKeyControl");
        if (_isAwaitingInput) // since only one KeyControl can await input at the moment we know that it's this one
        {
            _isAwaitingInput = false;
            isAwaitingInput = false;
            return _action(NUMBERS.IndexOf(key.KeyChar));
        }
        if(!isAwaitingInput && key.Key == _key) // when no one is awaiting input and the key matches
        {
            _isAwaitingInput = true;
            isAwaitingInput = true;
            return _promptAction;
        }
        return _baseKeyControl.Check(key, ref isAwaitingInput); // key doesn't match or someone awaits input

    }

    public NumberKeyControl(IKeyControl baseKeyControl, ConsoleKey key,  Func<int, IActionType> action, IActionType promptAction)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
        _promptAction = promptAction;
    }
}