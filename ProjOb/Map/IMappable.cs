namespace ProjOb;

public interface IMappable
{
    Tile? Position { get; set; }
    char Display { get; }
    string Name { get; }
    public IItem? ToItem();
}