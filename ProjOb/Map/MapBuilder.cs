using System.Drawing;
using ProjOb.Currencies;
using ProjOb.Elixirs;
using ProjOb.WeaponEffects;

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
        Random r =  new Random();
        for(int i = 0; i < n; i++)
        {
            Tile t = _map![r.Next() % MapSizeX,  r.Next() % MapSizeY];
            if (t.IsWalkable == false)
            {
                --i;
                continue;
            }
            yield return t;
        }
    }

    public void AddPaths()
    {
        CheckNull();
        throw new NotImplementedException();
    }

    public void AddRooms()
    {
        CheckNull();
        throw new NotImplementedException();
    }

    public void AddMainRoom()
    {
        CheckNull();
        throw new NotImplementedException();
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
           tile.Add(item);
        }
    }

    public void AddWeapons(int n)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            Weapon weapon = _r.NextDouble() switch
            {
                < 0.3 => new Dagger(),
                < 0.7 => new SmallSword(),
                _ => new Longsword()
            };
            tile.Add(weapon);
        }
    }

    public void AddEffectWeapons(int n)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            IWeapon weapon = _r.NextDouble() switch
            {
                < 0.3 => new Dagger(),
                < 0.7 => new SmallSword(),
                _ => new Longsword()
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
            tile.Add(weapon);
        }
    }

    public void AddElixirs(int n)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            IElixir elixir = _r.NextDouble() switch
            {
                < 0.3 => new PowerElixir(),
                < 0.6 => new AgilityElixir(),
                < 0.9 => new HealthElixir(),
                _ => new StrongPowerElixir()
            };
            tile.Add(elixir);
        }
    }

    public void AddCurrencies(int n, int max)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            ICurrency currency = _r.NextDouble() switch
            {
                < 0.3 => new Money(_r.Next(100)),
                _ => new Gold(_r.Next(100))
            };
            tile.Add(currency);
        }
    }

    public void AddEnemies(int n)
    {
        CheckNull();
        foreach (Tile tile in GetTiles(n))
        {
            Enemy enemy = _r.NextDouble() switch
            {
                < 0.3 => new Goblin(tile),
                < 0.7 => new Kobold(tile),
                _ => new Ogre(tile)
            };
            tile.Add(enemy);
        }
    }

    public Map? GetResult()
    {
        _map?.UpdateOnUpdates();
        return _map;
    }
}