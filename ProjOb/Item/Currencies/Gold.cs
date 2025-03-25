namespace ProjOb.Currencies;

public class Gold : Item, ICurrency
{
    public int Amount { get; set; }
    public Player? PlayerOwner => Owner as Player;
    public override string Info => $"Amount: {Amount}";

    public Gold(int amount) : base("Gold", 'G')
    {
        Amount = amount;
        OnUse = () =>
        {
            PlayerOwner!.Currencies["Gold"].Amount += Amount;
            return (true, null);
        };
    }
}