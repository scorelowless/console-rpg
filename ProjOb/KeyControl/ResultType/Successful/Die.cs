namespace ProjOb.ResultType;

public class Die : IResultType
{
    public bool WasSuccessful => true;
    public bool IsSenderDead => true;
    public string Message => "Player died";
}