namespace ProjOb.WeaponEffects;

public class Lucky : EffectWeapon
{
    private const int Value = 5;
    public Lucky(IWeapon weapon) : base(weapon, " (Lucky)")
    {
        OnUseCustom = () =>
        {
            Weapon.Owner!.Stats["Luck"] += Value;
            return (true, this);
        };
        OnUnequipCustom = () => Weapon.Owner!.Stats["Luck"] -= Value;
    }
}