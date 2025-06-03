using System.Drawing;

namespace ProjOb;

public class Map
{
    public int MapSizeX { get; set; }
    public int MapSizeY { get; set; }
    public List<List<Tile>> Tiles { get; set; } = null!;
    public List<Enemy?> Enemies { get; set; } = [];
    public Tile this[int x, int y] => Tiles[x][y];

    public Map(List<List<Tile>> tiles, int x, int y)
    {
        Tiles = tiles;
        MapSizeX = x;
        MapSizeY = y;
    }

    public Map()
    {
    }

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
            case Direction.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction));
        }

        if (position.X < 0 || position.X >= MapSizeX || position.Y < 0 || position.Y >= MapSizeY ||
            !Tiles[position.X][position.Y].IsNotWall)
        {
            return tile;
        }
        return Tiles[position.X][position.Y];
    }

    public List<IResultType> UpdateEnemies(Player player)
    {
        List<IResultType> results = [];
        foreach (Enemy? enemy in Enemies)
        {
            IResultType? result = enemy?.ExecuteStrategy(player);
            if (result != null) 
            {
                results.Add(result);
            }
        }
        return results;
    }

    public int CheckDistance(Tile t1, Tile t2)
    {
        return Math.Abs(t1.Position.X - t2.Position.X) + Math.Abs(t1.Position.Y - t2.Position.Y);
    }
}