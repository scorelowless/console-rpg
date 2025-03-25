namespace ProjOb;

public class Goblin : Enemy
{
    public Goblin(Tile position) : base("Goblin", 'g', position)
    {
        Grab(new Dagger(null));
        Grab(new Dagger(null));
    }
}