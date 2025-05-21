using System.Text.Json.Serialization;
using ProjOb.ActionType;

namespace ProjOb;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Die), "Die")]
[JsonDerivedType(typeof(DropEverythingNow), "DropEverythingNow")]
[JsonDerivedType(typeof(Move), "Move")]
[JsonDerivedType(typeof(Invalid), "None")]
[JsonDerivedType(typeof(Unequip), "Unequip")]
[JsonDerivedType(typeof(Attack), "Attack")]
[JsonDerivedType(typeof(PickUp), "PickUp")]
[JsonDerivedType(typeof(ThrowAway), "ThrowAway")]
[JsonDerivedType(typeof(Use), "Use")]
[JsonDerivedType(typeof(AddPlayer), "AddPlayer")]
public interface IActionType
{
    int PlayerIndex { get; set; }
    IResultType Execute(Model model);
}