namespace ProjOb;

public class Inventory
{
    private const int InventorySize = 20;
    private readonly LinkedList<IItem> _inventory = [];
    public LinkedListNode<IItem>? CurrentItem { get; private set; }
    public IEnumerable<IItem> Get => _inventory;
    public bool IsFull => _inventory.Count >= InventorySize;
    public void Add(IItem item)
    {
        if(IsFull) return;
        _inventory.AddLast(item);
        if(_inventory.Count == 1)
            SelectedItemIncrement();
    }
    public int ItemCount => _inventory.Count;
    
    public IItem? RemoveSelected()
    {
        if(CurrentItem == null) return null;
        LinkedListNode<IItem> node = CurrentItem;
        _inventory.Remove(CurrentItem);
        CurrentItem = node.Next ?? _inventory.First;
        return node.Value;
    }
    
    public void SelectedItemIncrement()
    {
        if(_inventory.Count == 0) return;
        CurrentItem = CurrentItem == null ? _inventory.First : CurrentItem.Next ?? _inventory.First;
    }
    
    public void SelectedItemDecrement()
    {
        if(_inventory.Count == 0) return;
        CurrentItem = CurrentItem == null ? _inventory.First : CurrentItem.Previous ?? _inventory.Last;
    }
}