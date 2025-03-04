namespace ProjOb.WeaponEffects;

public class Lucky : EffectWeapon
{
    private const int Value = 5;
    public Lucky(Weapon weapon) : base(weapon, " (Lucky)")
    {
        if(Weapon.Owner != null && Weapon.IsHeld)
            Weapon.Owner.Stats[AttributeName.Luck] += Value;
    }
}