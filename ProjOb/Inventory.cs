namespace ProjOb;

public class Inventory
{
    private const int InventorySize = 20;
    private readonly List<IItem> _inventory = [];
    public IEnumerable<IItem> Get => _inventory;
    public bool IsFull => _inventory.Count >= InventorySize;
    public void Add(IItem item)
    {
        if(IsFull) return;
        _inventory.Add(item);
    }
    public IItem? this[int index] => index < ItemCount ? _inventory[index] : null;
    public int ItemCount => _inventory.Count;
    
    public IItem? RemoveSelected(int ind)
    {
        if (ItemCount <= ind) return null;
        var item = _inventory[ind];
        _inventory.RemoveAt(ind);
        return item;
    }
}