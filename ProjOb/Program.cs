namespace ProjOb;

static class Program
{
    private static bool _isRunning = true;
    private static readonly Player Player;
    private static void Main()
    {
        Thread keyListenerThread = new Thread(KeyListener);
        keyListenerThread.Start();
    }

    static Program()
    {
        Map map = MapBuilderDirector.GenerateMap();
        Player = new Player(map);
        _ = Display.GetInstance(map, Player);
    }

    private static void KeyListener()
    {
        while (_isRunning)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                
                switch (key)
                {
                    case ConsoleKey.A:
                        Player.Move(Direction.Left);
                        break;
                    case ConsoleKey.W:
                        Player.Move(Direction.Up);
                        break;
                    case ConsoleKey.S:
                        Player.Move(Direction.Down);
                        break;
                    case ConsoleKey.D:
                        Player.Move(Direction.Right);
                        break;
                    case ConsoleKey.E:
                        Player.PickUp();
                        break;
                    case ConsoleKey.R:
                        Player.Use();
                        break;
                    case ConsoleKey.T:
                        Player.Unequip();
                        break;
                    case ConsoleKey.Q:
                        Player.ThrowAway();
                        break;
                    case ConsoleKey.OemComma:
                        Player.SelectedItemDecrement();
                        break;
                    case ConsoleKey.OemPeriod:
                        Player.SelectedItemIncrement();
                        break;
                    case ConsoleKey.Escape:
                        _isRunning = false;
                        break;
                }
            }
        }
    }
}