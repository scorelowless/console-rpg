using System.Drawing;

namespace ProjOb;

public class MapBuilder : IMapBuilder
{
    private Map? _map;
    private readonly Random _r = new();
    public const int MapSizeX = 40;
    public const int MapSizeY = 20;
    
    public void Reset()
    {
        _map = null;
    }

    public void Empty()
    { 
        Tile[,] tiles = new Tile[MapSizeX, MapSizeY];
        _map = new Map(tiles, MapSizeX, MapSizeY);
        for (int i = 0; i < MapSizeX; i++)
        {
            for (int j = 0; j < MapSizeY; j++)
            {
                tiles[i, j] = new Tile(_map, true, new Point(i, j));
            }
        }
    }

    public void Full()
    {
        Tile[,] tiles = new Tile[MapSizeX, MapSizeY];
        _map = new Map(tiles, MapSizeX, MapSizeY);
        for (int i = 0; i < MapSizeX; i++)
        {
            for (int j = 0; j < MapSizeY; j++)
            {
                tiles[i, j] = new Tile(_map, false, new Point(i, j));
            }
        }
    }

    private void CheckNull()
    {
        if (_map == null) throw new ArgumentNullException($"{nameof(MapBuilder)}.map");
    }

    private IEnumerable<Tile> GetTiles(int n)
    {
        CheckNull();
        for(int i = 0; i < n; i++)
        {
            Tile t = _map![_r.Next(MapSizeX), _r.Next(MapSizeY)];
            if (t.IsNotWall == false)
            {
                --i;
                continue;
            }
            yield return t;
        }
    }

    private void AddPath(Point start, Point end)
    {
        // random shortest path between two points
        bool goesDown = start.X < end.X;
        bool goesRight = start.Y < end.Y;
        int sizeX = int.Abs(start.X - end.X);
        int sizeY = int.Abs(start.Y - end.Y);
        int n = sizeX + sizeY;
        Point current = new Point(start.X, start.Y);
        for (int i = 0; i < n; ++i)
        {
            _map![current.X, current.Y].IsNotWall = true;
            if (_r.Next(n - i) > sizeX - 1)
            {
                current.Y += goesRight ? 1 : -1;
                sizeY--;
            }
            else
            {
                current.X += goesDown ? 1 : -1;
                sizeX--;
            }
        }
        _map![current.X, current.Y].IsNotWall = true;
    }

    public void AddDefaultPath()
    {
        AddPath(new Point(0, 0), new Point(10, 5));
    }
    public void AddPaths(int n)
    {
        CheckNull();
        for (int i = 0; i < n; i++)
        {
            switch (_r.Next(4))
            {
                case 0:
                    AddPath(new Point(_r.Next(MapSizeX / 2),
                            _r.Next(MapSizeY)),
                        new Point(_r.Next(MapSizeX / 2),
                            _r.Next(MapSizeY)));
                    break; // in the left part
                case 1:
                    AddPath(new Point(_r.Next(MapSizeX / 2) + MapSizeX / 2,
                            _r.Next(MapSizeY)),
                        new Point(_r.Next(MapSizeX / 2) + MapSizeX / 2,
                            _r.Next(MapSizeY)));
                    break; // in the right part
                case 2:
                    AddPath(new Point(_r.Next(MapSizeX),
                            _r.Next(MapSizeY / 2)),
                        new Point(_r.Next(MapSizeX),
                            _r.Next(MapSizeY / 2)));
                    break; // in the upper part
                case 3:
                    AddPath(new Point(_r.Next(MapSizeX),
                            _r.Next(MapSizeY / 2) + MapSizeY / 2),
                        new Point(_r.Next(MapSizeX),
                            _r.Next(MapSizeY / 2) + MapSizeY / 2));
                    break; // in the lower part
            }
        }
    }

    public void AddRooms(int n)
    {
        CheckNull();
        for(int i = 0; i < n; i++)
        {
            Tile t = _map![_r.Next(MapSizeX),  _r.Next(MapSizeY)];
            if (t.IsNotWall)
            {
                --i;
                continue;
            }
            t.IsNotWall = true;
        }
    }

    public void AddMainRoom()
    {
        CheckNull();
        for (int i = 10; i < MapSizeX - 10; i++)
        {
            for (int j = 5; j < MapSizeY - 5; j++)
            {
                _map![i, j].IsNotWall = true;
            }
        }
    }

    public void AddItems(int n)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
           double v = _r.NextDouble();
           Item item = v switch
           {
               < 0.3 => new Bottle(),
               < 0.7 => new Wood(),
               _ => new Stone()
           };
           tile.AddItem(item);
        }
    }

    public void AddWeapons(int n)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            Weapon weapon = _r.NextDouble() switch
            {
                < 0.25 => new Dagger(),
                < 0.5 => new SmallSword(),
                < 0.75 => new Longsword(),
                _ => new Staff()
            };
            tile.AddItem(weapon);
        }
    }

    public void AddEffectWeapons(int n)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            IWeapon weapon = _r.NextDouble() switch
            {
                < 0.25 => new Dagger(),
                < 0.5 => new SmallSword(),
                < 0.75 => new Longsword(),
                _ => new Staff()
            };
            weapon = _r.NextDouble() switch
            {
                < 0.3 => new Lucky(weapon),
                _ => weapon
            };
            weapon = _r.NextDouble() switch
            {
                < 0.3 => new Wise(weapon),
                _ => weapon
            };
            weapon = _r.NextDouble() switch
            {
                < 0.3 => new Strong(weapon),
                < 0.7 => weapon,
                _ => new Weak(weapon)
            };
            tile.AddItem(weapon);
        }
    }

    public void AddElixirs(int n)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            ElixirItem elixirItem = _r.NextDouble() switch
            {
                < 0.3 => new PowerElixirItem(),
                < 0.6 => new AgilityElixirItem(),
                < 0.9 => new HealthElixirItem(),
                _ => new Milk()
            };
            tile.AddItem(elixirItem);
        }
    }

    public void AddCurrencies(int n, int max)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            Currency currency = _r.NextDouble() switch
            {
                < 0.3 => new Money(_r.Next(100)),
                _ => new Gold(_r.Next(100))
            };
            tile.AddItem(currency);
        }
    }

    public void AddEnemies(int n)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            Enemy enemy = _r.NextDouble() switch
            {
                < 0.25 => new Goblin(tile),
                < 0.5 => new Kobold(tile),
                < 0.75 => new Mage(tile),
                _ => new Ogre(tile)
            };
        }
    }

    public object? GetResult()
    {
        _map?.UpdateOnUpdates();
        return _map;
    }
}