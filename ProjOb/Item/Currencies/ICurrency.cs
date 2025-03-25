namespace ProjOb.Currencies;

public interface ICurrency : IItem
{
    int Amount { get; set; }
    int Index { get; }
    Player? PlayerOwner { get; }
}