namespace ProjOb;

public interface IResultType
{
    bool WasSuccessful => true;
    bool WasAttack => false;
    string Message { get; }
}