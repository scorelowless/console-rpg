namespace ProjOb;

public abstract class ElixirItem : Item
{
    protected ElixirItem(string name, char display) : base(name, display, ConsoleColor.Magenta)
    {
        
    }
}