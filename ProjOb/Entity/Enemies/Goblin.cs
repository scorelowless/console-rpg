namespace ProjOb;

public class Goblin : Enemy
{
    public Goblin(Tile position) : base("Goblin", 'g', position)
    {
        SetStats(5,5,5,5,5,10, 5);
        Grab(new SmallSword());
        HeldItems[0]?.OnPickUp(this);
    }
    public override void Attack(int _, Entity target)
    {
        HeldItems[0]?.ToWeapon()?.Attack(new NormalAttack(this, target));
    }
}