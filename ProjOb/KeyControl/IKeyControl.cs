namespace ProjOb;

public interface IKeyControl
{
    public int Check(ConsoleKeyInfo key);
    // return values:
    // 0 - success
    // 1 - control reached GuardKeyControl
    // 2 - Inventory full
    // 3 - Tile empty
    // 4 - Invalid inventory slot selected
}