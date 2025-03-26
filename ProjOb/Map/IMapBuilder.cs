namespace ProjOb;

public interface IMapBuilder
{
    void Reset();
    void Empty();
    void Full();
    void AddPaths(int n);
    void AddDefaultPath();
    void AddRooms(int n);
    void AddMainRoom();
    void AddItems(int n);
    void AddWeapons(int n);
    void AddEffectWeapons(int n);
    void AddElixirs(int n);
    void AddCurrencies(int n, int max);
    void AddEnemies(int n);
    Map? GetResult();
}