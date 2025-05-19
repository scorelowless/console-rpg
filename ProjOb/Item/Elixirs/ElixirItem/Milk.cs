namespace ProjOb;

public class Milk : ElixirItem
{
    public Milk() : base("Milk", 'M')
    {
        
    }

    public override (bool, IItem?) OnUse()
    {
        foreach (IEffect effect in Owner!.Effects)
        {
            Owner.RemoveEffect(effect);
        }
        return (true, null);
    }
}