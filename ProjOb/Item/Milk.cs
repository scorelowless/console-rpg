using ProjOb.Tours.ElixirEffect;

namespace ProjOb;

public class Milk : Item
{
    public Milk() : base("Milk", 'M')
    {
        OnUse = () =>
        {
            foreach (IEffect effect in Owner!.Effects)
            {
                Owner.RemoveEffect(effect);
            }
            return (true, null);
        };
    }
}