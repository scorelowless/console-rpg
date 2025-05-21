namespace ProjOb;

public interface IResultType
{
    bool WasSuccessful { get; }
    bool IsSenderDead { get; }
    string Message { get; }
}