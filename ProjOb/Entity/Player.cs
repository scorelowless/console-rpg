namespace ProjOb;

public class Player : Entity
{
    public LastAction LastAction { get; private set; } = new(LastAction.ActionType.None);
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
    public int Move(Direction direction)
    {
        Tile nextPosition = Position.Map.NextTile(Position, direction);
        if (nextPosition == Position) return ReturnCode.CANT_MOVE;
        if (nextPosition.ContainsEnemies() != null) return ReturnCode.CANT_MOVE;
        Position.RemovePlayer();
        Position = nextPosition;
        Position.AddPlayer(this);
        UpdateNearbyEnemy();
        LastAction = direction switch
        {
            Direction.Up => new LastAction(LastAction.ActionType.MoveUp, encounteredEnemy: NearbyEnemy),
            Direction.Right => new LastAction(LastAction.ActionType.MoveRight, encounteredEnemy: NearbyEnemy),
            Direction.Down => new LastAction(LastAction.ActionType.MoveDown, encounteredEnemy: NearbyEnemy),
            Direction.Left => new LastAction(LastAction.ActionType.MoveLeft, encounteredEnemy: NearbyEnemy),
            _ => new LastAction(LastAction.ActionType.None),
        };
        NextTour();
        return ReturnCode.SUCCESS;
    }

    public int PickUp(int ind)
    {
        if (Inventory.IsFull)
        {
            return ReturnCode.INVENTORY_FULL;
        }
        var temp = Position.Pick(ind);
        if (temp == null) return ReturnCode.TILE_EMPTY;
        temp.OnPickUp(this);
        Inventory.Add(temp);
        LastAction = new LastAction(LastAction.ActionType.PickUp, [temp.Name]);
        NextTour();
        return ReturnCode.SUCCESS;
    }
    
    public int ThrowAway(int ind)
    {
        IItem? item = Inventory.RemoveSelected(ind);
        if (item == null) return ReturnCode.INVALID_INVENTORY_SLOT;
        Position.AddItem(item);
        item.OnThrow();
        LastAction = new LastAction(LastAction.ActionType.ThrowAway, [item.Name]);
        NextTour();
        return ReturnCode.SUCCESS;
    }

    public int DropEverythingNow()
    {
        while (Inventory.ItemCount > 0)
        {
            IItem? item = Inventory.RemoveSelected(0);
            Position.AddItem(item!);
            item!.OnThrow();
        }
        LastAction = new LastAction(LastAction.ActionType.DropEverything);
        NextTour();
        return ReturnCode.SUCCESS;
    }

    public int Use(int ind)
    {
        IItem? I = Inventory[ind];
        if(I == null) return ReturnCode.INVALID_INVENTORY_SLOT;
        var res = I.OnUse(); // try to use item
        if (!res.Item1) return ReturnCode.CANT_USE; // if it cannot be used
        if (res.Item2 == null) // if item after use is used (null)
        {
            Inventory.RemoveSelected(ind);
            LastAction = new LastAction(LastAction.ActionType.Use, [I.Name]);
        }
        else
        {
            IHeldable? h = res.Item2.ToHeldable();
            if(h != null && Inventory.Grab(h)) // if the item is IHeldable and can be grabbed
            {
                Inventory.RemoveSelected(ind);
                LastAction = new LastAction(LastAction.ActionType.Equip, [I.Name]);
            }
        }
        NextTour();
        return ReturnCode.SUCCESS;
    }

    public int Unequip()
    {
        if (Inventory.IsFull)
        {
            return ReturnCode.INVENTORY_FULL;
        }
        var t = Inventory.Ungrab();
        if (t == null) return ReturnCode.NOTHING_HELD;
        t.OnUnequip();
        Inventory.Add(t);
        LastAction = new LastAction(LastAction.ActionType.Unequip, [t.Name]);
        NextTour();
        return ReturnCode.SUCCESS;
    }

    public int Attack(int type)
    {
        return Attack(type, NearbyEnemy);
    }

    protected override int Attack(int type, Entity? _)
    {
        if (NearbyEnemy == null) return ReturnCode.NO_ENEMY;
        Enemy target = NearbyEnemy;
        if (type < 0 || type > 2) return ReturnCode.WRONG_ATTACK;
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
        
        LastAction = new LastAction(LastAction.ActionType.Attack, [target.Name]);
        NextTour();
        UpdateNearbyEnemy();
        if (NearbyEnemy != target) return ReturnCode.SUCCESS;
        target.Attack(this);
        if (Stats[StatsType.Health] <= 0) return ReturnCode.DEATH;
        LastAction = new LastAction(LastAction.ActionType.Defense, [target.Name]);
        return ReturnCode.SUCCESS;
    }

    public override void ReceiveDamage(int damage)
    {
        Stats[StatsType.Health] -= int.Max(damage - Stats[StatsType.Armor], 0);
        if (Stats[StatsType.Health] <= 0)
        {
            Game.CurrentGame.Stop();
        }
    }
}