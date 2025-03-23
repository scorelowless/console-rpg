namespace ProjOb.Currencies;

public class Gold : Item, ICurrency
{
    public int Amount { get; set; }
    public int Index { get; }
    public override string Info => $"Amount: {Amount}";

    public Gold(int amount, int index, Tile? position = null) : base("Money", '$', position)
    {
        Amount = amount;
        Index = index;
        OnUse = () =>
        {
            Owner!.Currencies[Index].Amount += Amount;
            return (false, null);
        };
    }
}