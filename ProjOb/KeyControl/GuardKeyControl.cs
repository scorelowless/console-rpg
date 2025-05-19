namespace ProjOb;

public class GuardKeyControl : IKeyControl
{
    public int Check(ConsoleKeyInfo key)
    {
        return ReturnCode.GUARD_KEY_CONTROL;
    }
}