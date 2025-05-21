namespace ProjOb;

public class NumberKeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private const string NUMBERS = "123456789abcdefghijklmnopqrstuvwxyz";
    private readonly ConsoleKey _key;
    private readonly IIndexedActionType _action;
    private readonly string _prompt;
    private readonly Display _display;
    
    public IActionType Check(ConsoleKeyInfo key)
    {
        if (key.Key == _key)
        {
            _display.Prompt(_prompt);
            while (!_display.IsKeyAvailable())
            {
                Thread.Sleep(50);
            }
            var ind = _display.ReadKey();
            _display.HideCursor();
            int index = NUMBERS.IndexOf(ind.KeyChar);
            _action.Ind = index;
            return _action;
        }
        return _baseKeyControl.Check(key);
    }

    public NumberKeyControl(IKeyControl baseKeyControl, ConsoleKey key,  IIndexedActionType action, string prompt, Display display)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
        _prompt = prompt;
        _display = display;
    }
}