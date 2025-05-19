using System.Drawing;

namespace ProjOb;

public class Map
{
    private readonly int _mapSizeX;
    private readonly int _mapSizeY;
    private readonly Tile[,] _tiles;
    public Tile this[int x, int y] => _tiles[x, y];

    public Map(Tile[,] tiles, int x, int y)
    {
        _tiles = tiles;
        _mapSizeX = x;
        _mapSizeY = y;
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
            default:
                throw new ArgumentOutOfRangeException(nameof(direction));
        }

        if (position.X < 0 || position.X >= _mapSizeX || position.Y < 0 || position.Y >= _mapSizeY ||
            !_tiles[position.X, position.Y].IsNotWall)
        {
            return tile;
        }
        return _tiles[position.X, position.Y];
    }
}