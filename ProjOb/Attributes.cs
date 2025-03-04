namespace ProjOb;

public class Attributes
{
    private readonly Dictionary<AttributeName, int> _attributes = new()
    {
        { AttributeName.Power, 0 },
        { AttributeName.Agility, 0 },
        { AttributeName.Health, 0 },
        { AttributeName.Luck, 0 },
        { AttributeName.Aggression, 0 },
        { AttributeName.Wisdom, 0 }
    };

    public int this[AttributeName attributeName]
    {
        get => _attributes.GetValueOrDefault(attributeName, 0);
        set => _attributes[attributeName] = value;
    }
}