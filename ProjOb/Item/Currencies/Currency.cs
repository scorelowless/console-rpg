namespace ProjOb;

public abstract class Currency : Item
{
    public override string Info => $"Amount: {Amount}";
    protected Currency(string name, char display, int amount) : base(name, display, ConsoleColor.Yellow)
    {
        Amount = amount;
    }
    public int Amount { get; set; }
}