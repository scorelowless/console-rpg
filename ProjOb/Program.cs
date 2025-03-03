namespace ProjOb;

class Program
{
    static void Main(string[] args)
    {
        Map map = new Map();
        Player player = new Player(map);
        Display display = new Display(map,  player);
    }
}