namespace ProjOb;

public abstract class Enemy : Entity
{
    protected Enemy(string name, char display, Tile position) : base(name, display, position)
    {
        position.AddEnemy(this);
    }
}