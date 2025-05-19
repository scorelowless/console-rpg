using System.Drawing;

namespace ProjOb;

public class Display
{
    private static Display? _instance;
    private const int Width = 150;
    private const int Height = 40;
    private const int Offset = 5;
    private const string Numbers = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    private readonly Map _map;
    private readonly Player _player;
    private readonly string _instructions;
    
    private readonly Point _defaultCursorPos;
    private int _cursorTop;
    private int _cursorLeft;
    private readonly ColoredChar[,] _content = new ColoredChar[Width, Height];
    private ConsoleColor _currentColor = ConsoleColor.White;
    
    private Display(Map map, Player player, string instructions)
    {
        _map = map;
        _player = player;
        _instructions = instructions;
        _defaultCursorPos = new Point(0, 20 + 1 + instructions.Count(c => c == '\n') + 1 + 1);
        _instance = this;
        Console.SetWindowSize(Width,  Height);
        Console.CursorVisible = false;
        Clear();
        InitializeConsoleText();
    }

    public static Display GetInstance(Map? map = null, Player? player = null, string instructions = "")
    {
        if(_instance == null && (map == null || player == null)) throw new Exception("Tried to instantiate a display without a map or a player.");
        return _instance ?? new Display(map!, player!, instructions);
    }
    
    private void SetCursor(int left, int top)
    {
        _cursorLeft = left;
        _cursorTop = top;
    }

    private void PutChar(ColoredChar c)
    {
        _content[_cursorLeft, _cursorTop] = c;
        _cursorLeft++;
    }
    private void Write(string text) // writes as if the beginnings of the line were at the cursor column
    {
        text += '\n';
        int offset = _cursorLeft;
        foreach (char c in text)
        {
            if (_cursorLeft >= Width)
            {
                _cursorLeft = offset;
                _cursorTop++;
            }
            if (_cursorTop >= Height) break;
            if (c == '\n')
            {
                _cursorLeft = offset;
                _cursorTop++;
                continue;
            }
            _content[_cursorLeft, _cursorTop] = new ColoredChar(c);
            _cursorLeft++;
        }
    }

    private void Print()
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < Height; ++i)
        {
            for (int j = 0; j < Width; ++j)
            {
                if (_currentColor != _content[j, i].Color)
                {
                    Console.ForegroundColor = _currentColor = _content[j, i].Color;
                }
                Console.Write(_content[j, i].Character == 0 ? ' ' : _content[j, i].Character);
            }
            Console.WriteLine();
        }
    }

    private void ClearArea(int width, int height)
    {
        for (int i = _cursorLeft; i < _cursorLeft + width; i++)
        {
            for (int j = _cursorTop; j < _cursorTop + height; j++)
            {
                _content[i, j] = new ColoredChar(' ');
            }
        }
    }
    
    public static void Clear()
    {
        Console.Clear();
    }
    
    private void InitializeConsoleText()
    {
        SetCursor(0, 0);
        DisplayMap();
        SetCursor(0, 21);
        DisplayInstructions();
        SetCursor(MapBuilder.MapSizeX + Offset, 0);
        DisplayPlayer();
        SetCursor(0, _defaultCursorPos.Y - 1);
        Log("");
        SetCursor(100, 0);
        UpdateInventory();
        Print();
    }

    private void UpdateTile(Point p)
    {
        SetCursor(p.X, p.Y);
        PutChar(_map[p.X, p.Y].Print());
    }

    private void UpdateWholePlayer()
    {
        SetCursor(MapBuilder.MapSizeX + Offset, 0);
        ClearArea(50, 40);
        DisplayPlayer();
        SetCursor(100, 0);
        ClearArea(50, 40);
        UpdateInventory();
        Print();
    }

    public void Update()
    {
        SetCursor(0,0);
        DisplayMap();
        UpdateWholePlayer();
    }

    private void DisplayInstructions()
    {
        Write(_instructions);
    }

    public void Log(string message)
    {
        SetCursor(0, _defaultCursorPos.Y - 1);
        ClearArea(50, 2);
        Write("Log:");
        Write(message);
        Print();
    }
    
    private void DisplayPlayer()
    {
        foreach (var stat in _player.Stats)
        {
            Write($"{stat.Key}: {stat.Value}");
        }

        Write("------------------------------------------");

        foreach (var currency in _player.Currencies)
        {
            Write($"{currency.Value.Name}: {currency.Value.Amount}");
        }

        Write("------------------------------------------");
        Write($"Left hand: {_player.Inventory.HeldItemsHands[0]?.Name ?? "Nothing"}");
        Write($"Right hand: {_player.Inventory.HeldItemsHands[1]?.Name ?? "Nothing"}");
        if (_player.Effects.Count != 0)
        {
            Write("------------------------------------------");
            foreach (IEffect effect in _player.Effects)
            {
                Write($"{effect.Name}: {effect.ToursLeft} tours left");
            }
        }
        if (_player.Position.ContainsItems)
        {
            Write("------------------------------------------");
            Write("Contents of the tile:");
            int num = 0;
            foreach (var item in _player.Position)
            {
                if (item.Name == "Player") continue;
                Write($"{Numbers[num]}. {item.Name}");
                num++;
            }
        }
        if (_player.NearbyEnemy != null)
        {
            Write("------------------------------------------");
            Write($"Nearby Enemy: {_player.NearbyEnemy.Name}");
            Write($"  Health: {_player.NearbyEnemy.Stats[Entity.StatsType.Health]}");
            Write($"  Damage: {_player.NearbyEnemy.Inventory.HeldItemsHands[0]?.ToWeapon()?.Damage ?? 0}");
            Write($"  Armor: {_player.NearbyEnemy.Stats[Entity.StatsType.Armor]}");
        }
    }

    private void UpdateInventory()
    {
        Write("Inventory:");
        int num = 0;
        foreach (var item in _player.Inventory.Get)
        {
            Write($"{Numbers[num]}. {item.Name}");
            if(item.Info != "")
                Write("  " + item.Info);
            num++;
        }
    }

    private void DisplayMap()
    {
        for (int y = 0; y < MapBuilder.MapSizeY; y++)
        {
            for (int x = 0; x < MapBuilder.MapSizeX; x++)
            {
                PutChar(_map[x, y].Print());
            }
            SetCursor(0, y + 1);
        }
    }
    
    public void Prompt(string text)
    {
        SetCursor(0, _defaultCursorPos.Y - 1);
        ClearArea(100, 2);
        Write(text);
        Print();
    }

    public void ShowCursor(int linesOfPrompt)
    {
        Console.SetCursorPosition(0, _defaultCursorPos.Y + linesOfPrompt - 1);
        Console.CursorVisible = true;
    }

    public void HideCursor()
    {
        Console.CursorVisible = false;
    }

    public ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey(true);
    }

    public void GameOver()
    {
        SetCursor(0, 0);
        ClearArea(Width, Height);
        Write("""
              
              
              
              
              
              
              
              
              
              
                                              ▄██████▄     ▄████████   ▄▄▄▄███▄▄▄▄      ▄████████       ▄██████▄   ▄█    █▄     ▄████████    ▄████████ 
                                             ███    ███   ███    ███ ▄██▀▀▀███▀▀▀██▄   ███    ███      ███    ███ ███    ███   ███    ███   ███    ███ 
                                             ███    █▀    ███    ███ ███   ███   ███   ███    █▀       ███    ███ ███    ███   ███    █▀    ███    ███ 
                                            ▄███          ███    ███ ███   ███   ███  ▄███▄▄▄          ███    ███ ███    ███  ▄███▄▄▄      ▄███▄▄▄▄██▀ 
                                           ▀▀███ ████▄  ▀███████████ ███   ███   ███ ▀▀███▀▀▀          ███    ███ ███    ███ ▀▀███▀▀▀     ▀▀███▀▀▀▀▀   
                                             ███    ███   ███    ███ ███   ███   ███   ███    █▄       ███    ███ ███    ███   ███    █▄  ▀███████████ 
                                             ███    ███   ███    ███ ███   ███   ███   ███    ███      ███    ███ ███    ███   ███    ███   ███    ███ 
                                             ████████▀    ███    █▀   ▀█   ███   █▀    ██████████       ▀██████▀   ▀██████▀    ██████████   ███    ███ 
                                                                                                                                            ███    ███ 
              """);
        Print();
    }
}