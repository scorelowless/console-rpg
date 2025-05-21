namespace ProjOb;

public class Model
{
    public List<Player> Players { get; set; } = null!;
    public Map Map { get; set; } = null!;
    public string Instructions { get; set; } = null!;

    public Model(List<Player> players, Map map, string instructions)
    {
        Players = players;
        Map = map;
        Instructions = instructions;
    }

    public Model()
    {
    }
}