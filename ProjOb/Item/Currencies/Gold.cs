namespace ProjOb.Currencies;

public class Gold : Item, ICurrency
{
    public int Amount { get; set; }
    public int Index { get; }
    public Player? PlayerOwner => Owner as Player;
    public override string Info => $"Amount: {Amount}";

    public Gold(int amount, int index) : base("Money", '$')
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