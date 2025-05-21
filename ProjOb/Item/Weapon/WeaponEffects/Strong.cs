namespace ProjOb;

public class Strong : EffectWeapon
{
    private const int VALUE = 5;
    public override int Damage => Weapon.Damage + VALUE;

    public Strong(IWeapon weapon) : base(weapon, " (Strong)")
    {
        
    }
    
    public Strong()
    {
    }
}