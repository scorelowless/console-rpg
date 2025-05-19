namespace ProjOb;

public class Inventory
{
    private readonly List<IHeldable> _heldItems = [];
    private int _freeHands = 2;
    private const int InventorySize = 20;
    private readonly List<IItem> _inventory = [];
    public IEnumerable<IItem> Get => _inventory;
    public List<IHeldable?> HeldItemsHands
    {
        get
        {
            if (_freeHands == 2) return [null, null];
            if (_freeHands == 1) return [_heldItems[0], null];
            if (_heldItems[0].HandsTaken == 2) return [_heldItems[0], null];
            return [_heldItems[0], _heldItems[1]];
        }
    }
    
    public List<IHeldable> HeldItemsList => _heldItems;

    public bool IsFull => _inventory.Count >= InventorySize;
    public void Add(IItem item)
    {
        if(IsFull) return;
        _inventory.Add(item);
    }
    public IItem? this[int index] => index < ItemCount && index >= 0 ? _inventory[index] : null;
    public int ItemCount => _inventory.Count;
    
    public IItem? RemoveSelected(int ind)
    {
        if (ItemCount <= ind || ind < 0) return null;
        var item = _inventory[ind];
        _inventory.RemoveAt(ind);
        return item;
    }
    
    public bool Grab(IHeldable heldable)
    {
        if(heldable.HandsTaken > _freeHands)
        {
            return false;
        }
        _heldItems.Add(heldable);
        _freeHands -= heldable.HandsTaken;
        return true;
    }

    public IHeldable? Ungrab()
    {
        return _heldItems.Count != 0 ? _heldItems.PopBack() : null;
    }
}