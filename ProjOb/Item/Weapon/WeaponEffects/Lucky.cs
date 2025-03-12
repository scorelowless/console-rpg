namespace ProjOb.WeaponEffects;

public class Lucky : EffectWeapon
{
    private const int Value = 5;
    public Lucky(Weapon weapon) : base(weapon, " (Lucky)")
    {
        OnGrabCustom = () => Weapon.Owner!.Stats[AttributeName.Luck] += Value;
        OnUngrabCustom = () => Weapon.Owner!.Stats[AttributeName.Luck] -= Value;
    }
}