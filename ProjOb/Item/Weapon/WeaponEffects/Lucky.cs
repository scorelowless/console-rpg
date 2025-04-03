namespace ProjOb;

public class Lucky : EffectWeapon
{
    private const int Value = 5;
    public Lucky(IWeapon weapon) : base(weapon, " (Lucky)")
    {
        
    }

    public override (bool, IItem?) OnUse()
    {
        Weapon.Owner!.Stats[Entity.StatsType.Luck] += Value;
        return base.OnUse();
    }

    public override void OnUnequip()
    {
        base.OnUnequip();
        Weapon.Owner!.Stats[Entity.StatsType.Luck] -= Value;
    }
}