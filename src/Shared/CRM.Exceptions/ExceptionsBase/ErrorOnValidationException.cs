namespace CRM.Exceptions.ExceptionsBase;
public class ErrorOnValidationException : CRMException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errors)
    {
        ErrorMessages = errors;
    }
}
