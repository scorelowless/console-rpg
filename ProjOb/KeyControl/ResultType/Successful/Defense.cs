namespace ProjOb.ResultType;

public class Defense : IResultType
{
    private readonly IResultType _attack1;
    private readonly IResultType _attack2;
    public Defense(IResultType attack1, IResultType attack2)
    {
        _attack1 = attack1;
        _attack2 = attack2;
    }
    public bool WasSuccessful => true;
    public bool IsSenderDead => false;
    public string Message => $"{_attack1.Message}\n{_attack2.Message}";
}