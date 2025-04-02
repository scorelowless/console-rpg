using System.Collections.Immutable;
using ProjOb.Currencies;
using ProjOb.Tours;
using ProjOb.Tours.ElixirEffect;

namespace ProjOb;

public class Player : Entity
{
    private const int InventorySize = 20;
    public int SelectedItem { get; private set; }
    
    // TODO: split OnUpdate into array of events and update every element individually
    public event Action? OnUpdate;
    private readonly IItem?[] _inventory;
    public LastAction LastAction { get; private set; } = new(LastAction.ActionType.None);
    public Enemy? NearbyEnemy { get; private set; }
    public ImmutableArray<IItem?> Inventory => [.._inventory];

    public Dictionary<string, ICurrency> Currencies { get; }

    public Player(Map map) : base("Player", '¶', map[0,0])
    {
        _inventory = new IItem?[InventorySize];
        Currencies = new Dictionary<string, ICurrency>
        {
            { "Money", new Money(0) },
            { "Gold", new Gold(0) }
        };
        map[0, 0].AddPlayer();
    }

    public void Move(Direction direction)
    {
        Tile nextPosition = Position.Map.NextTile(Position, direction);
        if (nextPosition == Position) return;
        if (nextPosition.ContainsEnemies() != null) return;
        Position.RemovePlayer();
        Position = nextPosition;
        Position.AddPlayer();
        NearbyEnemy = Position.Map.NextTile(Position, Direction.Up).ContainsEnemies() ??
                      Position.Map.NextTile(Position, Direction.Right).ContainsEnemies() ??
                      Position.Map.NextTile(Position, Direction.Down).ContainsEnemies() ??
                      Position.Map.NextTile(Position, Direction.Left).ContainsEnemies() ??
                      null;
        LastAction = direction switch
        {
            Direction.Up => new LastAction(LastAction.ActionType.MoveUp, encounteredEnemy: NearbyEnemy),
            Direction.Right => new LastAction(LastAction.ActionType.MoveRight, encounteredEnemy: NearbyEnemy),
            Direction.Down => new LastAction(LastAction.ActionType.MoveDown, encounteredEnemy: NearbyEnemy),
            Direction.Left => new LastAction(LastAction.ActionType.MoveLeft, encounteredEnemy: NearbyEnemy),
            _ => new LastAction(LastAction.ActionType.None),
        };
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
        if (temp == null) return;
        temp.OnPickUp(this);
        _inventory[ind] = temp;
        LastAction = new LastAction(LastAction.ActionType.PickUp, temp);
        OnUpdate?.Invoke();
    }

    public void ThrowAway()
    {
        if (Inventory[SelectedItem] == null) return;
        Position.AddItem(Inventory[SelectedItem]!);
        Inventory[SelectedItem]!.OnThrow();
        LastAction = new LastAction(LastAction.ActionType.ThrowAway, Inventory[SelectedItem]);
        _inventory[SelectedItem] = null;
        OnUpdate?.Invoke();
    }

    public void Use()
    {
        IItem? I = Inventory[SelectedItem];
        if (I == null) return;
        var res = I.OnUse(); // try to use item
        if (!res.Item1) return; // if it cannot be used
        LastAction = new LastAction(LastAction.ActionType.Use, I);
        if (res.Item2 == null) // if item after use is used (null)
        {
            _inventory[SelectedItem] = null;
            OnUpdate?.Invoke();
            return;
        }
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
        LastAction = new LastAction(LastAction.ActionType.Unequip, t);
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