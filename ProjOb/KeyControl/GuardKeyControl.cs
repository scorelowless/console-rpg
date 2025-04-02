namespace ProjOb;

public class GuardKeyControl : IKeyControl
{
    public void Check(ConsoleKey key)
    {
        Display.Log("This key does nothing");
    }
}