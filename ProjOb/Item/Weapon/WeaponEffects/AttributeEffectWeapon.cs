namespace ProjOb.WeaponEffects;

public abstract class AttributeEffectWeapon : EffectWeapon
{
    private bool _isActive;
    private Attributes  _attributes;
    private AttributeName _attributeName;
    
    
    public AttributeEffectWeapon(Weapon weapon, string effectName) : base(weapon, effectName)
    {
        
    }

    public override void Grab()
    {
        base.Grab();
        _isActive = true;
    }
}