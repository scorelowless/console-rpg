using ProjOb;

namespace ProjOb;

public class Player : Entity
{
    // TODO: split OnUpdate into array of events and update every element individually
    public event Action? OnUpdate;
    private readonly LinkedList<IItem> _inventory = [];
    public LastAction LastAction { get; private set; } = new(LastAction.ActionType.None);
    public Enemy? NearbyEnemy { get; private set; }
    public Inventory Inventory { get; } = new();
    public Dictionary<string, Currency> Currencies { get; }

    public Player(Map map) : base("Player", '¶', map[0,0])
    {
        Currencies = new Dictionary<string, Currency>
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
        NextTour();
        OnUpdate?.Invoke();
    }

    public void PickUp()
    {
        if (Inventory.IsFull)
        {
            ProjOb.Display.Log("Inventory full!");
            return;
        }
        var temp = Position.Pick();
        if (temp == null) return;
        temp.OnPickUp(this);
        Inventory.Add(temp);
        LastAction = new LastAction(LastAction.ActionType.PickUp, temp);
        NextTour();
        OnUpdate?.Invoke();
    }
    
    public void ThrowAway()
    {
        IItem? item = Inventory.RemoveSelected();
        if (item == null) return;
        Position.AddItem(item);
        item.OnThrow();
        LastAction = new LastAction(LastAction.ActionType.ThrowAway, item);
        NextTour();
        OnUpdate?.Invoke();
    }

    public void DropEverythingNow()
    {
        while (_inventory.Count > 0)
        {
            IItem? item = Inventory.RemoveSelected();
            Position.AddItem(item!);
            item!.OnThrow();
        }
        LastAction = new LastAction(LastAction.ActionType.DropEverything);
        NextTour();
        OnUpdate?.Invoke();
    }

    public void Use()
    {
        if (Inventory.CurrentItem == null) return;
        IItem I = Inventory.CurrentItem.Value;
        var res = I.OnUse(); // try to use item
        if (!res.Item1) return; // if it cannot be used
        LastAction = new LastAction(LastAction.ActionType.Use, I);
        if (res.Item2 == null) // if item after use is used (null)
        {
            Inventory.RemoveSelected();
        }
        else
        {
            IHeldable? h = res.Item2.ToHeldable();
            if(h != null && Grab(h)) // if the item is IHeldable and can be grabbed
            {
                Inventory.RemoveSelected();
            }
        }
        NextTour();
        OnUpdate?.Invoke();
    }

    public void Unequip()
    {
        if (Inventory.IsFull)
        {
            ProjOb.Display.Log("Inventory full!");
            return;
        }
        var t = Ungrab();
        if (t == null) return;
        t.OnUnequip();
        Inventory.Add(t);
        LastAction = new LastAction(LastAction.ActionType.Unequip, t);
        NextTour();
        OnUpdate?.Invoke();
    }
}