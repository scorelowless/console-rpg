namespace ProjOb.ResultType;

public class PlayerAdded : IResultType
{
    public bool WasSuccessful => true;
    public bool IsSenderDead => false;
    public string Message => "New player joined";
}