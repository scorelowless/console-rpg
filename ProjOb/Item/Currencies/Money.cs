namespace ProjOb.Currencies;

public class Money : Item, ICurrency
{
    public int Amount { get; set; }
    public Player? PlayerOwner => Owner as Player;
    public override string Info => $"Amount: {Amount}";

    public Money(int amount) : base("Money", '$')
    {
        Amount = amount;
        OnUse = () =>
        {
            PlayerOwner!.Currencies["Money"].Amount += Amount;
            return (true, null);
        };
    }
}