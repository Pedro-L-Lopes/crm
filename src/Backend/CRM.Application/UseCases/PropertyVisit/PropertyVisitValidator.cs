using CRM.Communication.Requests.PropertyVisit;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.PropertyVisit;

public class PropertyVisitValidator : AbstractValidator<RequestPropertyVisit>
{
    public PropertyVisitValidator()
    {
        RuleFor(v => v.PropertyId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.PROPERTY_ID_EMPTY);

        RuleFor(v => v.ClientId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.CLIENT_ID_EMPTY);

        RuleFor(v => v.AgentId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.AGENT_EMPTY);

        RuleFor(v => v.Status)
            .NotEmpty()
            .WithMessage(ResourceMessageException.STATUS_EMPTY);
    }
}
