namespace ProjOb;

public interface IKeyControl
{
    public IActionType Check(ConsoleKeyInfo key, ref bool isAwaitingInput);
}