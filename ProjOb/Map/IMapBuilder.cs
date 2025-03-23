namespace ProjOb;

public interface IMapBuilder
{
    void Reset();
    void Empty();
    void Full();
    void AddPaths();
    void AddRooms();
    void AddMainRoom();
    void AddItems();
    void AddWeapons();
    void AddEffectWeapons();
    void AddElixirs();
    void AddEnemies();
    void GetResult();
}

/*
   - puste podziemia - każdy element to pusty element,
   - wypełnione podziemia - każdy element to ściana,
   - dodanie ścieżek - dodaje losowe ścieżki przez podziemia,
   - dodanie komnat - dodaje losowe puste pola w podziemiach,
   - dodanie centralnego pomieszczenia - dodaje duże centralne pomieszczenie do labiryntu,
   - dodanie przedmiotów - rozkłada losowe przedmioty na polach nie będących ścianami,
   - dodanie broni - rozkłada losowe bronie na polach nie będących ścianami,
   - dodanie zmodyfikowanych broni - rozkłada losowe bronie z modyfikatorami na polach nie będących ścianami,
   - dodanie eliksirów - rozmieszcza losowe eliksiry na polach nie będących ścianami,
   - dodanie przeciwników - rozmieszcza losowych przeciwników na polach nie będących ścianami.
*/