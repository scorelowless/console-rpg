namespace ProjOb;

public class NumberKeyControl : IKeyControl
{
    private readonly IKeyControl _baseKeyControl;
    private const string Numbers = "123456789abcdefghijklmnopqrstuvwxyz";
    private readonly ConsoleKey _key;
    private readonly Action<int> _action;
    private readonly string _promptText;
    
    public void Check(ConsoleKeyInfo key)
    {
        char i;
        if (key.Key == _key && Numbers.Contains(i = Display.GetInstance().Prompt(_promptText).KeyChar))
        {
            _action(Numbers.IndexOf(i));
        }
        else
        {
            _baseKeyControl.Check(key);
        }
    }

    public NumberKeyControl(IKeyControl baseKeyControl, ConsoleKey key,  Action<int> action, string promptText)
    {
        _baseKeyControl = baseKeyControl;
        _key = key;
        _action = action;
        _promptText = promptText;
    }
}