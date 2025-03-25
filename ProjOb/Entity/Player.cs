using System.Collections.Immutable;
using ProjOb.Currencies;

namespace ProjOb;

public class Player : Entity
{
    private const int InventorySize = 20;
    public int SelectedItem { get; private set; }
    public event Action? OnUpdate;
    private readonly IItem?[] _inventory;
    public ImmutableArray<IItem?> Inventory => [.._inventory];
    public ICurrency[] Currencies { get; }

    public Player(Map map) : base("Player", '¶', map[0,0])
    {
        _inventory = new IItem?[InventorySize];
        Currencies = [new Money(0, 0), new  Gold(0, 1)];
    }

    public void Move(Direction direction)
    {
        Position.Remove(this);
        Position = Position.Map.NextTile(Position, direction);
        Position.Add(this);
        OnUpdate?.Invoke();
    }

    private int GetEmptyInventorySlot()
    {
        for(int i = 0; i < InventorySize; i++)
            if (Inventory[i] == null)
                return i;
        return -1;
    }

    public void PickUp()
    {
        int ind = GetEmptyInventorySlot();
        if (ind == -1) return;
        var temp = Position!.Pick();
        temp?.OnPickUp(this);
        _inventory[ind] = temp;
        OnUpdate?.Invoke();
    }

    public void ThrowAway()
    {
        if (Inventory[SelectedItem] == null) return;
        Position!.Add(Inventory[SelectedItem]!);
        Inventory[SelectedItem]!.OnThrow();
        _inventory[SelectedItem] = null;
        OnUpdate?.Invoke();
    }

    public void Use()
    {
        IItem? I = Inventory[SelectedItem];
        if (I == null) return;
        var res = I.OnUse(); // try to use item
        if (!res.Item1) return; // if it cannot be used
        if (res.Item2 == null) return; // if item after use is used (null)
        IHeldable? h = res.Item2.ToHeldable();
        if(h != null && Grab(h)) // if the item is IHeldable and can be grabbed
        {
            _inventory[SelectedItem] = null;
        }
        OnUpdate?.Invoke();
    }

    public void Unequip()
    {
        int ind = GetEmptyInventorySlot();
        if (ind == -1) return;
        var t = Ungrab();
        if (t == null) return;
        t.OnUnequip();
        _inventory[ind] = t;
        OnUpdate?.Invoke();
    }

    public void SelectedItemIncrement()
    {
        SelectedItem++;
        SelectedItem %= InventorySize;
        OnUpdate?.Invoke();
    }
    
    public void SelectedItemDecrement()
    {
        SelectedItem += InventorySize - 1;
        SelectedItem %= InventorySize;
        OnUpdate?.Invoke();
    }
}