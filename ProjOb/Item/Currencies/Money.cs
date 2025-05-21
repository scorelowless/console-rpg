namespace ProjOb;

public class Money : Currency
{
    public Money(int amount) : base("Money", '$', amount)
    {
        
    }

    public override (bool, IItem?) OnUse()
    {
        ((Player)Owner!).Currencies["Money"].Amount += Amount;
        return (true, null);
    }

    public Money()
    {
    }
}