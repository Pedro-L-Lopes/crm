namespace CRM.Exceptions.ExceptionsBase;

public class PlanExpiredException : CRMException
{
    public PlanExpiredException() : base(ResourceMessageException.INACTIVE_PLAN)
    {

    }
}
