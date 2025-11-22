using CRM.Communication.Requests;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.Tenant.Register;
public class RegisterTenantValidator : AbstractValidator<RequestRegisterTenantJson>
{
    public RegisterTenantValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .WithMessage(ResourceMessageException.NAME_EMPTY)
            .MinimumLength(3)
            .WithMessage(ResourceMessageException.NAME_LENGTH);

        RuleFor(t => t.Email)
            .NotEmpty()
            .WithMessage(ResourceMessageException.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourceMessageException.EMAIL_INVALID);

        RuleFor(t => t.Phone)
            .NotEmpty()
            .WithMessage(ResourceMessageException.NAME_EMPTY)
            .MinimumLength(3)
            .WithMessage(ResourceMessageException.NAME_LENGTH);

        RuleFor(t => t.Type)
            .NotEmpty()
            .WithMessage(ResourceMessageException.ACCOUNT_TYPE_EMPTY)
            .Must(type => type == "individual" || type == "agent" || type == "agency")
            .WithMessage(ResourceMessageException.ACCOUNT_TYPE_INVALID);

        RuleFor(t => t.PlanId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.PLAN_ID_EMPTY);
    }
}
