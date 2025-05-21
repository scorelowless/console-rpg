namespace ProjOb;

public class Weak : EffectWeapon
{
    private const int VALUE = -5;
    public override int Damage => Weapon.Damage + VALUE;

    public Weak(IWeapon weapon) : base(weapon, " (Weak)")
    {
    }
    
    public Weak()
    {
    }
}