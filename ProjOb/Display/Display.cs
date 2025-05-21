using System.Drawing;

namespace ProjOb;

public class Display
{
    private static Display? _instance;
    private const int WIDTH = 150;
    private const int HEIGHT = 40;
    private const int OFFSET = 5;
    private const string NUMBERS = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    private readonly Map _map = null!;
    private readonly Player _player = null!;
    private readonly string _instructions = null!;
    
    private readonly Point _defaultCursorPos;
    private int _cursorTop;
    private int _cursorLeft;
    private readonly ColoredChar[,] _content = new ColoredChar[WIDTH, HEIGHT];
    private ConsoleColor _currentColor = ConsoleColor.White;
    private readonly Mutex _consoleMutex = new Mutex();
    private readonly Mutex _bufferMutex = new Mutex();
    
    private Display(Map map, Player player, string instructions)
    {
        _map = map;
        _player = player;
        _instructions = instructions;
        _defaultCursorPos = new Point(0, 20 + 1 + instructions.Count(c => c == '\n') + 1 + 1);
        _instance = this;
        Console.SetWindowSize(WIDTH,  HEIGHT);
        Console.CursorVisible = false;
        Clear();
        InitializeConsoleText();
    }

    private Display()
    {
    }

    public static Display GetInstance(Map? map = null, Player? player = null, string instructions = "")
    {
        if (_instance == null && (map == null || player == null)) return new Display();
        return _instance ?? new Display(map!, player!, instructions);
    }
    
    private void SetCursor(int left, int top)
    {
        lock (_bufferMutex)
        {
            _cursorLeft = left;
            _cursorTop = top;
        }
    }

    private void PutChar(ColoredChar c)
    {
        lock (_bufferMutex)
        {
            _content[_cursorLeft, _cursorTop] = c;
            _cursorLeft++;
        }
    }
    private void Write(string text) // writes as if the beginnings of the line were at the cursor column
    {
        lock (_bufferMutex)
        {
            text += '\n';
            int offset = _cursorLeft;
            foreach (char c in text)
            {
                if (_cursorLeft >= WIDTH)
                {
                    _cursorLeft = offset;
                    _cursorTop++;
                }

                if (_cursorTop >= HEIGHT) break;
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
    }

    private void Print()
    {
        lock (_consoleMutex)
        {
            Console.SetCursorPosition(0, 0);
            lock (_bufferMutex)
            {
                for (int i = 0; i < HEIGHT; ++i)
                {
                    for (int j = 0; j < WIDTH; ++j)
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
        }
    }

    private void ClearArea(int width, int height)
    {
        lock (_bufferMutex)
        {
            for (int i = _cursorLeft; i < _cursorLeft + width; i++)
            {
                for (int j = _cursorTop; j < _cursorTop + height; j++)
                {
                    _content[i, j] = new ColoredChar(' ');
                }
            }
        }
    }

    public void Clear()
    {
        lock (_consoleMutex)
        {
            Console.Clear();
        }
    }
    
    private void InitializeConsoleText()
    {
        SetCursor(0, 0);
        DisplayMap();
        SetCursor(0, 21);
        DisplayInstructions();
        SetCursor(MapBuilder.MapSizeX + OFFSET, 0);
        DisplayPlayer();
        SetCursor(0, _defaultCursorPos.Y - 1);
        Log("");
        SetCursor(100, 0);
        UpdateInventory();
        Print();
    }

    /*private void UpdateTile(Point p)
    {
        SetCursor(p.X, p.Y);
        PutChar(_map[p.X, p.Y].Print());
    }*/

    private void UpdateWholePlayer()
    {
        SetCursor(MapBuilder.MapSizeX + OFFSET, 0);
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
        ClearArea(100, 5);
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
        if (_player.Position.ContainsItems())
        {
            Write("------------------------------------------");
            Write("Contents of the tile:");
            int num = 0;
            foreach (var item in _player.Position)
            {
                if (item.Name == "Player") continue;
                Write($"{NUMBERS[num]}. {item.Name}");
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
            Write($"{NUMBERS[num]}. {item.Name}");
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
        ClearArea(100, 5);
        Write(text);
        int lines = text.Count(c => c == '\n') + 1;
        /*lock (_consoleMutex)
        {
            Console.CursorVisible = true;
        }*/
        Print();
        lock (_consoleMutex)
        {
            Console.SetCursorPosition(0, _defaultCursorPos.Y + lines - 1);
        }
    }

    public void HideCursor()
    {
        //Console.CursorVisible = false;
    }

    public ConsoleKeyInfo ReadKey()
    {
        ConsoleKeyInfo key;
        lock (_consoleMutex)
        {
            key = Console.ReadKey(true);
        }
        return key;
    }

    public bool IsKeyAvailable()
    {
        bool ret;
        lock (_consoleMutex)
        {
            ret = Console.KeyAvailable;
        }
        return ret;
    }

    public void WriteError(string text)
    {
        Clear();
        lock (_consoleMutex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
        }
    }

    public void GameOver()
    {
        SetCursor(0, 0);
        ClearArea(WIDTH, HEIGHT);
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
        Console.SetCursorPosition(0, _defaultCursorPos.Y);
    }
}