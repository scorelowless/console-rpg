namespace ProjOb;

public abstract class Enemy : Entity
{
    protected Enemy(string name, char display, Tile position) : base(name, display, position, ConsoleColor.DarkRed)
    {
        position.AddEnemy(this);
    }
}