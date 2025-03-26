namespace ProjOb;

public static class MapBuilderDirector
{
    public static object GenerateBasicMap(IMapBuilder builder)
    {
        builder.Full();
        builder.AddMainRoom();
        builder.AddRooms(30);
        builder.AddPaths(20);
        builder.AddDefaultPath();
        builder.AddEnemies(10);
        builder.AddCurrencies(10, 50);
        builder.AddElixirs(10);
        builder.AddItems(20);
        builder.AddWeapons(10);
        builder.AddEffectWeapons(3);
        return builder.GetResult() ?? throw new Exception("Unexpected behavior: MapBuilder returned null");
    }
}