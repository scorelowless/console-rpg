namespace ProjOb.Currencies;

public interface ICurrency : IItem
{
    int Amount { get; set; }
    Player? PlayerOwner { get; }
}