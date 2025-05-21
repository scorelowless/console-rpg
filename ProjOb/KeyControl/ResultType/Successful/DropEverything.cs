namespace ProjOb.ResultType;

public class DropEverything : IResultType
{
    public bool WasSuccessful => true;
    public bool IsSenderDead => false;
    public string Message => "Player dropped entire inventory";
}