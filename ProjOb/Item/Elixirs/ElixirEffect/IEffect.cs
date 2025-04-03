namespace ProjOb.Tours.ElixirEffect;

public interface IEffect : ITourWatch
{
    int ToursLeft { get; }
    string Name { get;  }
    void OnRemove();
}