namespace ProjOb;

public interface IAttackVisitor
{
    void AttackHeavy(IWeapon w);
    void AttackLight(IWeapon w);
    void AttackMagic(IWeapon w);

    int Armor { get; }
}