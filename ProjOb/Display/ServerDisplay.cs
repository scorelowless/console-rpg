namespace ProjOb;

public class ServerDisplay
{
    private static ServerDisplay? _instance;
    private readonly Mutex _mutex = new Mutex();

    private ServerDisplay()
    {
        _instance = this;
    }

    public static ServerDisplay GetInstance()
    {
        return _instance ?? new ServerDisplay();
    }

    public void WriteInfo(string message)
    {
        lock (_mutex)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }
    }
    
    public void WriteError(string message)
    {
        lock (_mutex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }
    }

    public void WriteSuccess(string message)
    {
        lock (_mutex)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }
    }

    public void WriteUnsuccess(string message)
    {
        lock (_mutex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
        }
    }
}