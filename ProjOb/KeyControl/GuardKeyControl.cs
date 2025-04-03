namespace ProjOb;

public class GuardKeyControl : IKeyControl
{
    public void Check(ConsoleKeyInfo key)
    {
        Display.Log("This key does nothing");
    }
}