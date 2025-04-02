using System.Drawing;
using System.Text;

namespace ProjOb;

public class Display
{
    private static Display? _instance;
    private const int Offset = 5;
    private readonly Map _map;
    private readonly Player _player;
    private readonly string _instructions;
    private static readonly (int X, int Y) DefaultCursorPos = (0, 30);
    private static readonly (int X, int Y) MaxSize = (200, 40);
    private Display(Map map, Player player, string instructions)
    {
        _map = map;
        _player = player;
        _instructions = instructions;
        player.OnUpdate += Update;
        //map.OnUpdate += UpdateTile;
        _instance = this;
        Console.SetWindowSize(MaxSize.X,  MaxSize.Y);
        Update();
    }

    public static Display GetInstance(Map map, Player player, string instructions = "")
    {
        return _instance ?? new Display(map, player, instructions);
    }

    private void Update() // TODO: update only map/stats/eq/... and not the whole screen
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        foreach (var line in DisplayMap())
        {
            Console.WriteLine(line);
        }
        UpdateInstructions();
        UpdatePlayer();
    }

    private void UpdateTile(Point p)
    {
        Console.SetCursorPosition(p.X, p.Y);
        Console.Write(_map[p.X, p.Y].Print() );
        Console.SetCursorPosition(DefaultCursorPos.X, DefaultCursorPos.Y);
    }
    private void UpdatePlayer()
    {
        Console.SetCursorPosition(0, 0);
        foreach (string line in DisplayPlayer())
        {
            Console.SetCursorPosition(MapBuilder.MapSizeX + Offset, Console.CursorTop);
            Console.WriteLine(line);
        }
        Console.SetCursorPosition(0, 21);
        Console.WriteLine("Last action:");
        Console.WriteLine(_player.LastAction);
        Console.SetCursorPosition(DefaultCursorPos.X, DefaultCursorPos.Y);
    }

    private void UpdateInstructions()
    {
        Console.SetCursorPosition(0, 24);
        Console.Write(_instructions);
    }

    public static void Log(string message)
    {
        Console.SetCursorPosition(0, 22);
        Console.WriteLine(message);
        Console.SetCursorPosition(DefaultCursorPos.X, DefaultCursorPos.Y);
    }
    
    private IEnumerable<string> DisplayPlayer()
    {
        foreach (var stat in _player.Stats)
        {
            yield return $"{stat.Key}: {stat.Value}";
        }

        yield return "------------------------------------------";

        foreach (var currency in _player.Currencies)
        {
            yield return $"{currency.Value.Name}: {currency.Value.Amount}";
        }

        yield return "------------------------------------------";
        yield return $"Left hand: {_player.HeldItems[0]?.Name ?? "Nothing"}";
        yield return $"Right hand: {_player.HeldItems[1]?.Name ?? "Nothing"}";
        yield return "------------------------------------------";
        yield return "Contents of the tile:";
        foreach (var item in _player.Position)
        {
            if (item.Name == "Player") continue;
            yield return item.Name;
        }
        if (_player.NearbyEnemy != null)
        {
            yield return "------------------------------------------";
            yield return $"Nearby Enemy: {_player.NearbyEnemy.Name}";
        }
        
        yield return "------------------------------------------";
        yield return $"Inventory: (Selected item: {_player.Inventory[_player.SelectedItem]?.Name ?? "Nothing"})";
        foreach (var item in _player.Inventory)
        {
            if (item == null) continue;
            yield return item.Name;
            if(item.Info != "")
                yield return "  " + item.Info;
        }
    }

    private IEnumerable<string> DisplayMap()
    {
        for (int y = 0; y < MapBuilder.MapSizeY; y++)
        {
            StringBuilder sb = new();
            for (int x = 0; x < MapBuilder.MapSizeX; x++)
            {
                sb.Append(_map[x, y].Print());
            }
            yield return sb.ToString();
        }
    }
    
}