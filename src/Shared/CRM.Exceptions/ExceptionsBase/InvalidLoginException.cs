namespace CRM.Exceptions.ExceptionsBase;
public class InvalidLoginException : CRMException
{
    public InvalidLoginException() : base(ResourceMessageException.EMAIL_OR_PASSWORD_INVALID)
    {

    }
}
