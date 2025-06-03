namespace ProjOb.ResultType;

public class Unsuccessful : IResultType
{
    public bool WasSuccessful => false;
    public string Message { get; }

    private Unsuccessful(string message)
    {
        Message = message;
    }
    
    public static Unsuccessful CantMove() => new("Cannot move there!");
    public static Unsuccessful CantUse() => new("Cannot use this item!");
    public static Unsuccessful GuardKeyControl() => new("This key does nothing!");
    public static Unsuccessful HandsFull() => new("Cannot equip because hands are taken!");
    public static Unsuccessful InvalidInventorySlot() => new("Invalid inventory slot!");
    public static Unsuccessful InventoryFull() => new("Inventory is full!");
    public static Unsuccessful NoEnemy() => new("No enemy to attack!");
    public static Unsuccessful NothingHeld() => new("Nothing is held!");
    public static Unsuccessful TileEmpty() => new("Tile is empty!");
    public static Unsuccessful WrongAttack() => new("Invalid attack type!");
}