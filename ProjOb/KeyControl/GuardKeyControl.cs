namespace ProjOb;

public class GuardKeyControl : IKeyControl
{
    public void Check(ConsoleKeyInfo key)
    {
        Display.GetInstance().Log("This key does nothing!                  ");
    }
}