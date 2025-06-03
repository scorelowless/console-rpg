namespace ProjOb;

public class Inventory
{
    public List<IHeldable> HeldItems { get; set; } = [];
    public int FreeHands { get; set; } = 2;
    private const int INVENTORY_SIZE  = 20;
    public List<IItem> InventoryContent { get; set; } = [];
    public IEnumerable<IItem> Get => InventoryContent;
    public List<IHeldable?> HeldItemsHands
    {
        get
        {
            if (FreeHands == 2) return [null, null];
            if (FreeHands == 1) return [HeldItems[0], null];
            if (HeldItems[0].HandsTaken == 2) return [HeldItems[0], HeldItems[0]];
            return [HeldItems[0], HeldItems[1]];
        }
    }
    
    public List<IHeldable> HeldItemsList => HeldItems;

    public bool IsFull => InventoryContent.Count >= INVENTORY_SIZE;
    public void Add(IItem item)
    {
        if(IsFull) return;
        InventoryContent.Add(item);
    }
    public IItem? this[int index] => index < ItemCount && index >= 0 ? InventoryContent[index] : null;
    public int ItemCount => InventoryContent.Count;
    
    public IItem? RemoveSelected(int ind)
    {
        if (ItemCount <= ind || ind < 0) return null;
        var item = InventoryContent[ind];
        InventoryContent.RemoveAt(ind);
        return item;
    }
    
    public bool Grab(IHeldable heldable)
    {
        if(heldable.HandsTaken > FreeHands)
        {
            return false;
        }
        HeldItems.Add(heldable);
        FreeHands -= heldable.HandsTaken;
        return true;
    }

    public IHeldable? Ungrab()
    {
        return HeldItems.Count != 0 ? HeldItems.PopBack() : null;
    }
}