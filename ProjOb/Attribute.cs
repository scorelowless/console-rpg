namespace ProjOb;

public class Attribute
{
    public string Name { get; set; }
    public int Value { get; set; }
    public char Symbol { get; set; }

    public Attribute(string  name, char symbol, int value)
    {
        Name = name;
        Symbol = symbol;
        Value = value;
    }
}