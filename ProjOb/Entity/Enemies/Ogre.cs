namespace ProjOb;

public class Ogre : Enemy
{
    public Ogre(Tile position) : base("Ogre", 'o', position)
    {
        Grab(new Longsword(null));
    }
}