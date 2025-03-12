using System.Collections;
using System.Drawing;
using System.Text;
using ProjOb.WeaponEffects;

namespace ProjOb;

public class Map : IEnumerable<string>
{
    public const int XMapSize = 40;
    public const int YMapSize = 20;
    private readonly Tile[,] _tiles = new Tile[XMapSize, YMapSize];
    public event Action? OnUpdate;

    public Map()
    {
        GenerateMap();
    }

    public Tile this[int i, int j] => _tiles[i, j];
    
    public Tile NextTile(Tile tile, Direction direction)
    {
        Point position = new Point(tile.Position.X, tile.Position.Y);
        switch (direction)
        {
            case Direction.Up:
                position.Y--;
                break;
            case Direction.Down:
                position.Y++;
                break;
            case Direction.Left:
                position.X--;
                break;
            case Direction.Right:
                position.X++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction));
        }

        if (position.X < 0 || position.X >= XMapSize || position.Y < 0 || position.Y >= YMapSize ||
            !_tiles[position.X, position.Y].IsWalkable)
        {
            return tile;
        }
        return _tiles[position.X, position.Y];
    }

    public void GenerateMap()
    {
        for (var y = 0; y < YMapSize; y++)
        {
            for (var x = 0; x < XMapSize; x++)
            {
                _tiles[x,y] = new Tile(this, x % 2 == 0 || y % 2 == 1, new Point(x, y));
                _tiles[x, y].OnUpdate += () => OnUpdate?.Invoke();
            }
        }

        Random r = new();
        for (int i = 0; i < 100; i++)
        {
            int x = r.Next() % XMapSize;
            int y = r.Next() % YMapSize;
            Tile currentTile = _tiles[x, y];
            if(!currentTile.IsWalkable) continue;
            double f = r.NextDouble();
            switch (f)
            {
                case < 0.3:
                {
                    IWeapon l;
                    switch (f)
                    {
                        case < 0.1:
                            l = new Longsword(currentTile);
                            break;
                        case < 0.2:
                            l = new Dagger(currentTile);
                            f -= 0.1;
                            break;
                        default:
                            l = new SmallSword(currentTile);
                            f -= 0.2;
                            break;
                    }
                    if (f < 0.05) l = new Lucky(l);
                    if (f < 0.02) l = new Weak(l);
                    currentTile.Add(l);
                    break;
                }
                case < 0.5:
                {
                    Bottle b =  new Bottle(currentTile);
                    currentTile.Add(b);
                    break;
                }
                case < 0.7:
                {
                    Wood w = new Wood(currentTile);
                    currentTile.Add(w);
                    break;
                }
                case < 0.9:
                {
                    Stone s = new Stone(currentTile);
                    currentTile.Add(s);
                    break;
                }
            }
        }
    }

    public IEnumerator<string> GetEnumerator()
    {
        for (int y = 0; y < YMapSize; y++)
        {
            StringBuilder sb = new();
            for (int x = 0; x < XMapSize; x++)
            {
                sb.Append(_tiles[x, y].Print());
            }
            yield return sb.ToString();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}