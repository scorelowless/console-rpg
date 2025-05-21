namespace ProjOb;

public interface IAttackVisitor
{
    int AttackHeavy(IWeapon w);
    int AttackLight(IWeapon w);
    int AttackMagic(IWeapon w);

    int Armor { get; }
}