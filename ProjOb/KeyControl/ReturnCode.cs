// ReSharper disable InconsistentNaming
namespace ProjOb;

public static class ReturnCode // TODO: Convert to polymorphic classes
{
    public const int SUCCESS = 0;
    public const int GUARD_KEY_CONTROL = 1;
    public const int INVENTORY_FULL = 2;
    public const int TILE_EMPTY = 3;
    public const int INVALID_INVENTORY_SLOT = 4;
    public const int CANT_MOVE = 5;
    public const int CANT_USE = 6;
    public const int NOTHING_HELD = 7;
    public const int NO_ENEMY = 8;
    public const int WRONG_ATTACK = 9;
    public const int DEATH = 10;
}