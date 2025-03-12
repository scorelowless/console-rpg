namespace ProjOb;

public class Display
{
    public static int Offset = 5;
    private Map _map;
    private Player _player;

    public Display(Map map, Player player)
    {
        _map = map;
        _player = player;
        _player.OnUpdate += Update;
        _map.OnUpdate += Update;
        Update();
    }

    public void Update() // TODO: update only map/stats/eq/... and not the whole screen
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        foreach (var line in _map)
        {
            Console.WriteLine(line);
        }
        Console.SetCursorPosition(0, 0);
        foreach (var line in _player)
        {
            Console.SetCursorPosition(Map.XMapSize + Offset, Console.CursorTop);
            Console.WriteLine(line);
        }
    }
}