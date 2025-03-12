using System.Collections;

namespace ProjOb;

public class Player : Entity, IEnumerable<string>
{
    private const int InventorySize = 20;
    public int SelectedItem { get; set; }
    public Dictionary<string, int> Currencies { get; }
    public event Action? OnUpdate;

    public Player(Map map) : base("Player", map[0,0])
    {
        Inventory = new IMappable?[InventorySize];
        Currencies = new Dictionary<string, int>
        {
            { "Coins", 0 },
            { "Gold", 0 },
        };
        Display = '¶';
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
        var temp = Position.Pick();
        temp?.OnPickUp(this);
        Inventory[ind] = temp;
        OnUpdate?.Invoke();
    }

    public void ThrowAway()
    {
        if (Inventory[SelectedItem] == null) return;
        Position.Add(Inventory[SelectedItem]!);
        Inventory[SelectedItem]!.OnThrow(this);
        Inventory[SelectedItem] = null;
        OnUpdate?.Invoke();
    }

    public void Equip()
    {
        if (Inventory[SelectedItem] == null) return;
        if(Inventory[SelectedItem] is not IWeapon) return;
        if(Grab((IWeapon)Inventory[SelectedItem]!))
            Inventory[SelectedItem] = null;
        OnUpdate?.Invoke();
    }

    public void Unequip()
    {
        int ind = GetEmptyInventorySlot();
        if (ind == -1) return;
        
        if(HeldItems[1] != null)
        {
            Inventory[ind] = HeldItems[1];
            Ungrab(HeldItems[1]!);
        }
        else if (HeldItems[0] != null)
        {
            Inventory[ind] =  HeldItems[0];
            Ungrab(HeldItems[0]!);
        }
        else return;
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

    public IEnumerator<string> GetEnumerator()
    {
        foreach (var stat in Stats)
        {
            yield return $"{stat.Key}: {stat.Value}";
        }

        yield return "------------------------------------------";

        foreach (var currency in Currencies)
        {
            yield return $"{currency.Key}: {currency.Value}";
        }

        yield return "------------------------------------------";
        yield return $"Left hand: {HeldItems[0]?.Name ?? "Nothing"}";
        yield return $"Right hand: {HeldItems[1]?.Name ?? "Nothing"}";
        yield return "------------------------------------------";
        yield return "Contents of the tile:";
        foreach (var item in Position)
        {
            if (item.Name == "Player") continue;
            yield return item.Name;
        }
        yield return "------------------------------------------";
        yield return $"Inventory: (Selected item: {Inventory[SelectedItem]?.Name ?? "Nothing"})";
        foreach (var item in Inventory)
        {
            if (item == null) continue;
            yield return item.Name;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}