namespace ProjOb.WeaponEffects;

public class Wise : EffectWeapon
{
    private const int Value = 5;
    public Wise(IWeapon weapon) : base(weapon, " (Wise)")
    {
        OnGrabCustom = () => Weapon.Owner!.Stats["Wisdom"] += Value;
        OnUngrabCustom = () => Weapon.Owner!.Stats["Wisdom"] -= Value;
    }
}