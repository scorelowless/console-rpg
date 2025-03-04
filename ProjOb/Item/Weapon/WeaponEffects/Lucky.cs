namespace ProjOb.WeaponEffects;

public class Lucky : EffectWeapon
{
    private const int Value = 5;
    public Lucky(Weapon weapon) : base(weapon, " (Lucky)")
    {
        WhenGrabbed = () => Weapon.Owner!.Stats[AttributeName.Luck] += Value;
        WhenUngrabbed = () => Weapon.Owner!.Stats[AttributeName.Luck] -= Value;
    }

    public override Weapon RemoveEffect()
    {
        if(IsHeld)
            WhenUngrabbed();
        return base.RemoveEffect();
    }
}