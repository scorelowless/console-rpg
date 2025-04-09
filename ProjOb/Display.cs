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
    private readonly Point _defaultCursorPos;
    private static readonly Point MaxSize = new(200, 40);
    private const string Numbers = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static void SetCursor(Point p) => Console.SetCursorPosition(p.X, p.Y);
    private static void SetCursor(int x, int y) => Console.SetCursorPosition(x, y);
    private void ResetCursor() => SetCursor(_defaultCursorPos);
    
    private Display(Map map, Player player, string instructions)
    {
        _map = map;
        _player = player;
        _instructions = instructions;
        _defaultCursorPos = new(0, 20 + 1 + instructions.Count(c => c == '\n') + 1 + 1);
        player.OnUpdate += Update;
        //map.OnUpdate += UpdateTile;
        _instance = this;
        Console.SetWindowSize(MaxSize.X,  MaxSize.Y);
        Console.CursorVisible = false;
        Update();
    }

    public static Display GetInstance(Map map = null!, Player player = null!, string instructions = "")
    {
        if(_instance == null && (map == null || player == null)) throw new Exception("Tried to instantiate a display without a map or a player.");
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

    // private void UpdateTile(Point p)
    // {
    //     SetCursor(p);
    //     Console.Write(_map[p.X, p.Y].Print());
    //     ResetCursor();
    // }
    private void UpdatePlayer()
    {
        SetCursor(0, 0);
        foreach (string line in DisplayPlayer())
        {
            Console.SetCursorPosition(MapBuilder.MapSizeX + Offset, Console.CursorTop);
            Console.WriteLine(line);
        }
        SetCursor(0, _defaultCursorPos.Y - 1);
        Console.WriteLine("Log:");
        Console.WriteLine(_player.LastAction);
        SetCursor(100, 0);
        foreach (string line in DisplayInventory())
        {
            Console.SetCursorPosition(100, Console.CursorTop);
            Console.WriteLine(line);
        }
        ResetCursor();
    }

    private void UpdateInstructions()
    {
        SetCursor(0, 21);
        Console.Write(_instructions);
    }

    public void Log(string message)
    {
        ResetCursor();
        Console.WriteLine(message);
        ResetCursor();
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
        if (_player.Effects.Count != 0)
        {
            yield return "------------------------------------------";
            foreach (IEffect effect in _player.Effects)
            {
                yield return $"{effect.Name}: {effect.ToursLeft} tours left";
            }
        }
        if (_player.Position.ContainsItems)
        {
            yield return "------------------------------------------";
            yield return "Contents of the tile:";
            int num = 0;
            foreach (var item in _player.Position)
            {
                if (item.Name == "Player") continue;
                yield return $"{Numbers[num]}. {item.Name}";
                num++;
            }
        }
        if (_player.NearbyEnemy != null)
        {
            yield return "------------------------------------------";
            yield return $"Nearby Enemy: {_player.NearbyEnemy.Name}";
        }
    }

    private IEnumerable<string> DisplayInventory()
    {
        yield return "Inventory:";
        int num = 0;
        foreach (var item in _player.Inventory.Get)
        {
            yield return $"{Numbers[num]}. {item.Name}";
            if(item.Info != "")
                yield return "  " + item.Info;
            num++;
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
    
    public ConsoleKeyInfo Prompt(string text)
    {
        SetCursor(0, _defaultCursorPos.Y - 1);
        Console.WriteLine($"{text}:");
        Console.WriteLine("                                  ");
        ResetCursor();
        Console.CursorVisible = true;
        while (!Console.KeyAvailable) ;
        var key = Console.ReadKey(true);
        Console.CursorVisible = false;
        _instance!.Update();
        return key;
    }
}