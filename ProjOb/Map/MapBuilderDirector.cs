namespace ProjOb;

public static class MapBuilderDirector
{
    public static Map GenerateMap()
    {
        MapBuilder builder = new MapBuilder();
        builder.Empty();
        builder.AddEnemies(10);
        builder.AddCurrencies(10, 50);
        builder.AddElixirs(10);
        builder.AddItems(20);
        builder.AddWeapons(10);
        builder.AddEffectWeapons(3);
        return builder.GetResult() ?? throw new Exception("Unexpected behavior: MapBuilder returned null");
    }
}