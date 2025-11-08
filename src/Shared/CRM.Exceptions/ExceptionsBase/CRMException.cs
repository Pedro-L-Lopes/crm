namespace CRM.Exceptions.ExceptionsBase;
public class CRMException : SystemException
{
    public CRMException(string message) : base(message) { }
}
