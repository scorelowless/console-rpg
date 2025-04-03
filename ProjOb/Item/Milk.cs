using ProjOb;

namespace ProjOb;

public class Milk : Item
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