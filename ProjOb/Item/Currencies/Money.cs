namespace ProjOb.Currencies;

public class Money : Item, ICurrency
{
    public int Amount { get; set; }
    public int Index { get; }
    public Player? PlayerOwner => Owner as Player;
    public override string Info => $"Amount: {Amount}";

    public Money(int amount, int index, Tile? position = null) : base("Money", '$', position)
    {
        Amount = amount;
        Index = index;
        OnUse = () =>
        {
            PlayerOwner!.Currencies[Index].Amount += Amount;
            return (false, null);
        };
    }
}