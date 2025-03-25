namespace ProjOb;

public class Goblin : Enemy
{
    public Goblin(Tile position) : base("Goblin", 'g', position)
    {
        Grab(new Dagger());
        Grab(new Dagger());
    }
}