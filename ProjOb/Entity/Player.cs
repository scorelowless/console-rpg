namespace ProjOb;

public class Player : Entity
{
    public event Action? OnUpdate;

    public LastAction LastAction { get; private set; } = new(LastAction.ActionType.None);
    public Enemy? NearbyEnemy { get; private set; }
    public Inventory Inventory { get; } = new();
    public Dictionary<string, Currency> Currencies { get; } = new()
    {
        { "Money", new Money(0) },
        { "Gold", new Gold(0) }
    };

    public Player(Map map) : base("Player", '¶', map[0,0], ConsoleColor.Blue)
    {
        map[0, 0].AddPlayer(this);
    }

    public void Move(Direction direction)
    {
        Tile nextPosition = Position.Map.NextTile(Position, direction);
        if (nextPosition == Position) return;
        if (nextPosition.ContainsEnemies() != null) return;
        Position.RemovePlayer();
        Position = nextPosition;
        Position.AddPlayer(this);
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

    public void PickUp(int ind)
    {
        if (Inventory.IsFull)
        {
            ProjOb.Display.GetInstance().Log("Inventory full!");
            return;
        }
        var temp = Position.Pick(ind);
        if (temp == null) return;
        temp.OnPickUp(this);
        Inventory.Add(temp);
        LastAction = new LastAction(LastAction.ActionType.PickUp, temp);
        NextTour();
        OnUpdate?.Invoke();
    }
    
    public void ThrowAway(int ind)
    {
        IItem? item = Inventory.RemoveSelected(ind);
        if (item == null) return;
        Position.AddItem(item);
        item.OnThrow();
        LastAction = new LastAction(LastAction.ActionType.ThrowAway, item);
        NextTour();
        OnUpdate?.Invoke();
    }

    public void DropEverythingNow()
    {
        while (Inventory.ItemCount > 0)
        {
            IItem? item = Inventory.RemoveSelected(0);
            Position.AddItem(item!);
            item!.OnThrow();
        }
        LastAction = new LastAction(LastAction.ActionType.DropEverything);
        NextTour();
        OnUpdate?.Invoke();
    }

    public void Use(int ind)
    {
        IItem? I = Inventory[ind];
        if(I == null) return;
        var res = I.OnUse(); // try to use item
        if (!res.Item1) return; // if it cannot be used
        if (res.Item2 == null) // if item after use is used (null)
        {
            Inventory.RemoveSelected(ind);
            LastAction = new LastAction(LastAction.ActionType.Use, I);
        }
        else
        {
            IHeldable? h = res.Item2.ToHeldable();
            if(h != null && Grab(h)) // if the item is IHeldable and can be grabbed
            {
                Inventory.RemoveSelected(ind);
                LastAction = new LastAction(LastAction.ActionType.Equip, I);
            }
        }
        NextTour();
        OnUpdate?.Invoke();
    }

    public void Unequip()
    {
        if (Inventory.IsFull)
        {
            ProjOb.Display.GetInstance().Log("Inventory full!");
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