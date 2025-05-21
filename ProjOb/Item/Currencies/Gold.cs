namespace ProjOb;

public class Gold : Currency
{
    public Gold(int amount) : base("Gold", 'G', amount)
    {
        
    }
    public override (bool, IItem?) OnUse()
    {
        ((Player)Owner!).Currencies["Gold"].Amount += Amount;
        return (true, null);
    }

    public Gold()
    {
    }
}