namespace ProjOb.WeaponEffects;

public class Wise : EffectWeapon
{
    private const int Value = 5;
    public Wise(Weapon weapon) : base(weapon, " (Wise)")
    {
        WhenGrabbed = () => Weapon.Owner!.Stats[AttributeName.Wisdom] += Value;
        WhenUngrabbed = () => Weapon.Owner!.Stats[AttributeName.Wisdom] -= Value;
    }
}