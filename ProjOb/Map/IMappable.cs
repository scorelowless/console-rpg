namespace ProjOb;

public interface IMappable
{
    char Display { get; }
    string Name { get; }
    public IItem? ToItem();
}