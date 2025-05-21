using System.Text.Json.Serialization;

namespace ProjOb;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(AgilityElixirItem), "AgilityElixirItem")]
[JsonDerivedType(typeof(Bottle), "Bottle")]
[JsonDerivedType(typeof(Dagger), "Dagger")]
[JsonDerivedType(typeof(Gold), "Gold")]
[JsonDerivedType(typeof(HealthElixirItem), "HealthElixirItem")]
[JsonDerivedType(typeof(Longsword), "Longsword")]
[JsonDerivedType(typeof(Lucky), "Lucky")]
[JsonDerivedType(typeof(Milk), "Milk")]
[JsonDerivedType(typeof(Money), "Money")]
[JsonDerivedType(typeof(PowerElixirItem), "PowerElixirItem")]
[JsonDerivedType(typeof(SmallSword), "SmallSword")]
[JsonDerivedType(typeof(Staff), "Staff")]
[JsonDerivedType(typeof(Stone), "Stone")]
[JsonDerivedType(typeof(Strong), "Strong")]
[JsonDerivedType(typeof(Weak), "Weak")]
[JsonDerivedType(typeof(Wise), "Wise")]
[JsonDerivedType(typeof(Wood), "Wood")]
public interface IItem : IMappable
{
    public string Info { get; }
    public Entity? Owner { get; }
    public void OnPickUp(Entity entity);
    public void OnThrow();
    public (bool, IItem?) OnUse(); // returns if the item could be used/equipped and the item after use
    public void OnUnequip();
    public IHeldable? ToHeldable();
}