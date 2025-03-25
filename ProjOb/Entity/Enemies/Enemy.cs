namespace ProjOb;

public class Enemy : Entity
{
    public Enemy(string name, char display, Tile position) : base(name, display, position)
    {
        position.IsWalkable = false;
    }
}