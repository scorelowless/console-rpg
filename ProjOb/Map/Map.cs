namespace ProjOb;

public class Map
{
    private const int XMapSize = 20;
    private const int YMapSize = 40;
    private Tile[,] _tiles = new Tile[XMapSize, YMapSize];

    public Tile? NextTile(Tile tile, Direction direction)
    {
        throw new NotImplementedException();
    }
}