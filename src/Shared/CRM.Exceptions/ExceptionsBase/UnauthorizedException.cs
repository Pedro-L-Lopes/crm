using System.Net;

namespace CRM.Exceptions.ExceptionsBase;
public class UnauthorizedException : CRMException
{
    public UnauthorizedException(string message) : base(message)
    {
    }
    
    //public override IList<string> GetErrorMessages() => [Message];

    //public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}