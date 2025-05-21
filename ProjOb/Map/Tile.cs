using System.Drawing;

namespace ProjOb;

public class Tile
{
    public Map Map { get; set; } = null!;
    public List<IItem> Items { get; set; } = [];
    public List<Enemy> Enemies { get; set; } = [];
    public Player? Player { get; set; }
    public Point Position { get; set; }
    public bool IsNotWall { get; set; }
    public ColoredChar Print()
    {
        if (Player != null) return Player.Display;
        if (Enemies.Count != 0) return Enemies[^1].Display;
        if(Items.Count != 0) return Items[0].Display;
        if (!IsNotWall) return new ColoredChar('\u2588');
        return new ColoredChar(' ');
    }

    public Tile(Map map, bool isNotWall, Point position)
    {
        Map = map;
        IsNotWall = isNotWall;
        Position = position;
    }
    
    public Tile()
    {
    }

    public void AddItem(IItem item)
    {
        Items.Add(item);
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }

    public void AddPlayer(Player player)
    {
        Player = player;
    }

    public IItem? Pick(int ind)
    {
        if(Items.Count <= ind) return null;
        IItem item = Items[ind];
        Items.RemoveAt(ind);
        return item;
    }

    public void RemoveItem(IItem item)
    {
        Items.Remove(item);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }

    public void RemovePlayer()
    {
        Player = null;
    }
    
    public Enemy? ContainsEnemies() => Enemies.Count == 0 ? null : Enemies[0];

    public bool ContainsItems() => Items.Count != 0;
    
    public bool ContainsPlayer() => Player != null;
    
    public IEnumerator<IMappable> GetEnumerator() => Items.GetEnumerator();
}