namespace ProjOb;

public class Kobold : Enemy
{
    public Kobold(Tile position) : base("Kobold", 'k', position)
    {
        Grab(new SmallSword());
    }
}