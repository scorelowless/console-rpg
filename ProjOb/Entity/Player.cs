namespace ProjOb;

public class Player : Entity
{
    public Enemy? NearbyEnemy { get; private set; }
    public Dictionary<string, Currency> Currencies { get; } = new()
    {
        { "Money", new Money(0) },
        { "Gold", new Gold(0) }
    };

    public Player(Map map) : base("Player", '¶', map[0,0], ConsoleColor.Blue)
    {
        map[0, 0].AddPlayer(this);
        var r = new Random();
        SetStats(r.Next(5, 15), r.Next(5, 15), r.Next(5, 15), r.Next(5, 15), r.Next(5, 15), 50, 0);
        UpdateNearbyEnemy();
    }

    private void UpdateNearbyEnemy()
    {
        NearbyEnemy = Position.Map.NextTile(Position, Direction.Up).ContainsEnemies() ??
                      Position.Map.NextTile(Position, Direction.Right).ContainsEnemies() ??
                      Position.Map.NextTile(Position, Direction.Down).ContainsEnemies() ??
                      Position.Map.NextTile(Position, Direction.Left).ContainsEnemies() ??
                      null;
    }
    public IActionType Move(Direction direction)
    {
        Tile nextPosition = Position.Map.NextTile(Position, direction);
        if (nextPosition == Position || nextPosition.ContainsEnemies() != null || nextPosition.ContainsPlayer())
            return new ActionType.CantMove(this);
        Position.RemovePlayer();
        Position = nextPosition;
        Position.AddPlayer(this);
        UpdateNearbyEnemy();
        NextTour();
        return new ActionType.Move(this, direction, NearbyEnemy);
    }

    public IActionType PickUp(int ind)
    {
        if (Inventory.IsFull)
        {
            return new ActionType.InventoryFull(this);
        }
        var temp = Position.Pick(ind);
        if (temp == null) return new ActionType.TileEmpty(this);
        temp.OnPickUp(this);
        Inventory.Add(temp);
        NextTour();
        return new ActionType.PickUp(this, temp);
    }
    
    public IActionType ThrowAway(int ind)
    {
        IItem? item = Inventory.RemoveSelected(ind);
        if (item == null) return new ActionType.InvalidInventorySlot(this);
        Position.AddItem(item);
        item.OnThrow();
        NextTour();
        return new ActionType.ThrowAway(this, item);
    }

    public IActionType DropEverythingNow()
    {
        while (Inventory.ItemCount > 0)
        {
            IItem? item = Inventory.RemoveSelected(0);
            Position.AddItem(item!);
            item!.OnThrow();
        }
        NextTour();
        return new ActionType.DropEverything(this);
    }

    public IActionType Use(int ind)
    {
        IItem? I = Inventory[ind];
        if(I == null) return new ActionType.InvalidInventorySlot(this);
        var res = I.OnUse(); // try to use item
        if (!res.Item1) return new ActionType.CantUse(this); // if it cannot be used
        if (res.Item2 == null) // if item after use is used (null)
        {
            Inventory.RemoveSelected(ind);
            NextTour();
            return new ActionType.Use(this, I);
        }
        IHeldable? h = res.Item2.ToHeldable();
        if (h != null && Inventory.Grab(h)) // if the item is IHeldable and can be grabbed
        {
            Inventory.RemoveSelected(ind);
            NextTour();
            return new ActionType.Equip(this, I);
        }
        // if the item is IHeldable and there's no space in hands
        NextTour();
        return new ActionType.HandsFull(this);
    }

    public IActionType Unequip()
    {
        if (Inventory.IsFull)
        {
            return new ActionType.InventoryFull(this);
        }
        var t = Inventory.Ungrab();
        if (t == null) return new ActionType.NothingHeld(this);
        t.OnUnequip();
        Inventory.Add(t);
        NextTour();
        return new ActionType.Unequip(this, t);
    }

    public IActionType Attack(int type)
    {
        return Attack(type, NearbyEnemy);
    }

    protected override IActionType Attack(int type, Entity? _)
    {
        if (NearbyEnemy == null) return new ActionType.NoEnemy(this);
        Enemy target = NearbyEnemy;
        if (type < 0 || type > 2) return new ActionType.WrongAttack(this);
        var weap = Inventory.HeldItemsList.Select(heldable => heldable.ToWeapon()).ToList();
        weap.RemoveAll(w => w == null);

        IAttackVisitor visitor = type switch
        {
            0 => new NormalAttack(this, target),
            1 => new HiddenAttack(this, target),
            2 => new MagicAttack(this, target),
            _ => throw new Exception("Unexpected behavior in Player.Attack")
        };
        
        foreach (IWeapon? weapon in weap)
        {
            weapon?.Attack(visitor);
        }
        Stats[StatsType.Armor] = visitor.Armor;
        
        NextTour();
        UpdateNearbyEnemy();
        if (NearbyEnemy != target) return new ActionType.Attack(this, target);
        target.Attack(this);
        if (Stats[StatsType.Health] <= 0) return Die();
        return new ActionType.Defense(this, target);
    }

    public override void ReceiveDamage(int damage)
    {
        Stats[StatsType.Health] -= int.Max(damage - Stats[StatsType.Armor], 0);
    }

    public IActionType Die()
    {
        DropEverythingNow();
        Position.RemovePlayer();
        return new ActionType.Death(this);
    }
}