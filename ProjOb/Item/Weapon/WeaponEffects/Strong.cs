namespace ProjOb;

public class Strong : EffectWeapon
{
    private const int Value = 5;
    public override int Damage => Weapon.Damage + Value;

    public Strong(IWeapon weapon) : base(weapon, " (Strong)")
    {
        
    }
}