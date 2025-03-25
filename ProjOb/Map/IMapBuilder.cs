namespace ProjOb;

public interface IMapBuilder
{
    void Reset();
    void Empty();
    void Full();
    void AddPaths();
    void AddRooms();
    void AddMainRoom();
    void AddItems(int n);
    void AddWeapons(int n);
    void AddEffectWeapons(int n);
    void AddElixirs(int n);
    void AddCurrencies(int n);
    void AddEnemies(int n);
    Map? GetResult();
}