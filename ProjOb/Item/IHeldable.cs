using System.Text.Json.Serialization;

namespace ProjOb;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Dagger), "Dagger")]
[JsonDerivedType(typeof(Longsword), "Longsword")]
[JsonDerivedType(typeof(Lucky), "Lucky")]
[JsonDerivedType(typeof(SmallSword), "SmallSword")]
[JsonDerivedType(typeof(Staff), "Staff")]
[JsonDerivedType(typeof(Strong), "Strong")]
[JsonDerivedType(typeof(Weak), "Weak")]
[JsonDerivedType(typeof(Wise), "Wise")]
public interface IHeldable : IItem
{
    public int HandsTaken { get; }
    bool IsHeld { get; set; }
    public IWeapon? ToWeapon();
}