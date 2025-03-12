using System.Drawing;

namespace ProjOb;

public class Player : Entity
{
    private const int InventorySize = 20;
    public int SelectedItem { get; set; }
    public Currency[] Currencies { get; set; }

    public Player(Map map) : base(map)
    {
        HeldItems = new List<IHeldable>(2); // 0 - left hand, 1 - right hand
        Position = new Point(0, 0);
        Inventory = new Item[InventorySize];
        Currencies = new Currency[2];
        Currencies[0] = new Coins();
        Currencies[1] = new Gold();
    }

    void Move(Direction direction)
    {
        throw new NotImplementedException();
    }

    void PickUp(Tile tile)
    {
        throw new NotImplementedException();
    }

    void ThrowAway(Item item)
    {
        throw new NotImplementedException();
    }

    void Equip(IHeldable item)
    {
        throw new NotImplementedException();
    }
}