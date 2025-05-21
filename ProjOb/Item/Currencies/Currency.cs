using System.Text.Json.Serialization;

namespace ProjOb;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Gold), "Gold")]
[JsonDerivedType(typeof(Money), "Money")]
public abstract class Currency : Item
{
    public override string Info => $"Amount: {Amount}";
    protected Currency(string name, char display, int amount) : base(name, display, ConsoleColor.Yellow)
    {
        Amount = amount;
    }
    public int Amount { get; set; }

    public Currency()
    {
    }
}