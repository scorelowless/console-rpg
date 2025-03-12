using System.Drawing;

namespace ProjOb;

public class Map
{
    private const int XMapSize = 20;
    private const int YMapSize = 40;
    private Tile[,] _tiles = new Tile[XMapSize, YMapSize];

    public Tile? NextTile(Tile tile, Direction direction)
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

        if (position.X < 0 || position.X >= XMapSize || position.Y < 0 || position.Y >= YMapSize)
        {
            return tile;
        }
        return _tiles[position.X, position.Y];
    }

    public static Map GenerateMap()
    {
        Map map = new Map();
        for (var x = 0; x < XMapSize; x++)
        {
            for (var y = 0; y < YMapSize; y++)
            {
                map._tiles[x,y] = new Tile(x % 2 == 1 && y % 2 == 1, new Point(x, y));
            }
        }
        return map;
    }
}