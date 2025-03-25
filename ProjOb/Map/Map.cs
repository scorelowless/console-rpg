using System.Drawing;

namespace ProjOb;

public class Map
{
    private readonly int _mapSizeX;
    private readonly int _mapSizeY;
    private readonly Tile[,] _tiles;
    public Tile this[int x, int y] => _tiles[x, y];
    public event Action<Point>? OnUpdate;

    public Map(Tile[,] tiles, int x, int y)
    {
        _tiles = tiles;
        _mapSizeX = x;
        _mapSizeY = y;
    }

    public void UpdateOnUpdates()
    {
        for (var y = 0; y < _mapSizeY; y++)
        {
            for (var x = 0; x < _mapSizeX; x++)
            {
                int xx = x, yy = y;
                _tiles[x, y].OnUpdate += () => OnUpdate?.Invoke(new Point(xx, yy));
            }
        }
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
            !_tiles[position.X, position.Y].IsWalkable)
        {
            return tile;
        }
        return _tiles[position.X, position.Y];
    }
}