namespace ProjOb.WeaponEffects;

public class Lucky : EffectWeapon
{
    private const int Value = 5;
    public Lucky(IWeapon weapon) : base(weapon, " (Lucky)")
    {
        OnGrabCustom = () => Weapon.Owner!.Stats["Luck"] += Value;
        OnUngrabCustom = () => Weapon.Owner!.Stats["Luck"] -= Value;
    }
}