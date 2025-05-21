namespace ProjOb;

public class Lucky : EffectWeapon
{
    private const int VALUE = 5;
    public Lucky(IWeapon weapon) : base(weapon, " (Lucky)")
    {
        
    }

    public Lucky()
    {
    }

    public override (bool, IItem?) OnUse()
    {
        Weapon.Owner!.Stats[Entity.StatsType.Luck] += VALUE;
        return base.OnUse();
    }

    public override void OnUnequip()
    {
        base.OnUnequip();
        Weapon.Owner!.Stats[Entity.StatsType.Luck] -= VALUE;
    }
}