namespace ProjOb.WeaponEffects;

public class Wise : EffectWeapon
{
    private const int Value = 5;
    public Wise(Weapon weapon) : base(weapon, " (Wise)")
    {
        if(Weapon.Owner != null && Weapon.IsHeld)
            Weapon.Owner.Stats[AttributeName.Wisdom] += Value;
    }
}