namespace ProjOb;

public class Player : Entity
{
    public Enemy? NearbyEnemy { get; set; }
    public int Number { get; set; }
    public bool IsDead => Stats[StatsType.Health] <= 0;
    public Dictionary<string, Currency> Currencies { get; set; } = new()
    {
        { "Money", new Money(0) },
        { "Gold", new Gold(0) }
    };

    public Player(Map map, int number) : base("Player", (char)('1' + number), map[0,0], ConsoleColor.Cyan)
    {
        Number = number;
        map[0, 0].AddPlayer(this);
        var r = new Random();
        SetStats(r.Next(5, 15), r.Next(5, 15), r.Next(5, 15), r.Next(5, 15), r.Next(5, 15), 50, 0);
        UpdateNearbyEnemy();
    }

    public Player()
    {
    }

    public void UpdateNearbyEnemy()
    {
        NearbyEnemy = Position.Map.NextTile(Position, Direction.Up).ContainsEnemies() ??
                      Position.Map.NextTile(Position, Direction.Right).ContainsEnemies() ??
                      Position.Map.NextTile(Position, Direction.Down).ContainsEnemies() ??
                      Position.Map.NextTile(Position, Direction.Left).ContainsEnemies() ??
                      null;
    }
    public override IResultType Move(Direction direction)
    {
        Tile nextPosition = Position.Map.NextTile(Position, direction);
        if (nextPosition == Position || nextPosition.ContainsEnemies() != null || nextPosition.ContainsPlayer())
            return ResultType.Unsuccessful.CantMove();
        Position.RemovePlayer();
        Position = nextPosition;
        Position.AddPlayer(this);
        UpdateNearbyEnemy();
        NextTour();
        return new ResultType.Move(direction, NearbyEnemy);
    }

    public IResultType PickUp(int ind)
    {
        if (Inventory.IsFull)
        {
            return ResultType.Unsuccessful.InventoryFull();
        }
        var temp = Position.Pick(ind);
        if (temp == null) return ResultType.Unsuccessful.TileEmpty();
        temp.OnPickUp(this);
        Inventory.Add(temp);
        NextTour();
        return new ResultType.PickUp(temp);
    }
    
    public IResultType ThrowAway(int ind)
    {
        IItem? item = Inventory.RemoveSelected(ind);
        if (item == null) return ResultType.Unsuccessful.InvalidInventorySlot();
        Position.AddItem(item);
        item.OnThrow();
        NextTour();
        return new ResultType.ThrowAway(item);
    }

    public IResultType DropEverythingNow()
    {
        while (Inventory.ItemCount > 0)
        {
            IItem? item = Inventory.RemoveSelected(0);
            Position.AddItem(item!);
            item!.OnThrow();
        }
        NextTour();
        return new ResultType.DropEverything();
    }

    public IResultType Use(int ind)
    {
        IItem? I = Inventory[ind];
        if(I == null) return ResultType.Unsuccessful.InvalidInventorySlot();
        var res = I.OnUse(); // try to use item
        if (!res.Item1) return ResultType.Unsuccessful.CantUse(); // if it cannot be used
        if (res.Item2 == null) // if item after use is used (null)
        {
            Inventory.RemoveSelected(ind);
            NextTour();
            return new ResultType.Use(I);
        }
        IHeldable? h = res.Item2.ToHeldable();
        if (h != null && Inventory.Grab(h)) // if the item is IHeldable and can be grabbed
        {
            Inventory.RemoveSelected(ind);
            NextTour();
            return new ResultType.Equip(I);
        }
        // if the item is IHeldable and there's no space in hands
        NextTour();
        return ResultType.Unsuccessful.HandsFull();
    }

    public IResultType Unequip()
    {
        if (Inventory.IsFull)
        {
            return ResultType.Unsuccessful.InventoryFull();
        }
        var t = Inventory.Ungrab();
        if (t == null) return ResultType.Unsuccessful.NothingHeld();
        t.OnUnequip();
        Inventory.Add(t);
        NextTour();
        return new ResultType.Unequip(t);
    }

    public IResultType Attack(int type)
    {
        return Attack(type, NearbyEnemy);
    }

    protected override IResultType Attack(int type, Entity? _)
    {
        if (NearbyEnemy == null) return ResultType.Unsuccessful.NoEnemy();
        Enemy target = NearbyEnemy;
        if (type < 0 || type > 2) return ResultType.Unsuccessful.WrongAttack();
        var weap = Inventory.HeldItemsList.Select(heldable => heldable.ToWeapon()).ToList();
        weap.RemoveAll(w => w == null);

        IAttackVisitor visitor = type switch
        {
            0 => new NormalAttack(this, target),
            1 => new HiddenAttack(this, target),
            2 => new MagicAttack(this, target),
            _ => throw new Exception("Unexpected behavior in Player.Attack")
        };

        int damage = 0;
        foreach (IWeapon? weapon in weap)
        {
            damage += weapon?.Attack(visitor) ?? 0;
        }
        Stats[StatsType.Armor] = visitor.Armor;
        
        NextTour();
        UpdateNearbyEnemy();
        return new ResultType.Attack(this, target, damage);
    }

    public override void ReceiveDamage(int damage)
    {
        Stats[StatsType.Health] -= int.Max(damage - Stats[StatsType.Armor], 0);
        if (IsDead) Die();
    }

    public IResultType Die()
    {
        Stats[StatsType.Health] = 0;
        DropEverythingNow();
        Position.RemovePlayer();
        return new ResultType.Resign();
    }
}