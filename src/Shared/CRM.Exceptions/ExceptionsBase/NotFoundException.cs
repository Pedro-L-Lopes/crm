namespace CRM.Exceptions.ExceptionsBase;

public class NotFoundException : CRMException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
