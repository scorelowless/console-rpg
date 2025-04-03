namespace ProjOb;

public class Wise : EffectWeapon
{
    private const int Value = 5;
    public Wise(IWeapon weapon) : base(weapon, " (Wise)")
    {
        
    }
    public override (bool, IItem?) OnUse()
    {
        Weapon.Owner!.Stats[Entity.StatsType.Wisdom] += Value;
        return base.OnUse();
    }

    public override void OnUnequip()
    {
        base.OnUnequip();
        Weapon.Owner!.Stats[Entity.StatsType.Wisdom] -= Value;
    }
}