namespace ProjOb.WeaponEffects;

public class Wise : EffectWeapon
{
    private const int Value = 5;
    public Wise(IWeapon weapon) : base(weapon, " (Wise)")
    {
        OnUseCustom = () =>
        {
            Weapon.Owner!.Stats[Entity.StatsType.Wisdom] += Value;
            return (true, this);
        };
        OnUnequipCustom = () => Weapon.Owner!.Stats[Entity.StatsType.Wisdom] -= Value;
    }
}