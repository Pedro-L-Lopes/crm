using CRM.Communication.Requests;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(t => t.TenantId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.TENANT_REQUIRED);

        RuleFor(t => t.Name)
            .NotEmpty()
            .WithMessage(ResourceMessageException.NAME_EMPTY)
            .MinimumLength(3).MaximumLength(50)
            .WithMessage(ResourceMessageException.NAME_LENGTH);

        RuleFor(t => t.Email)
            .NotEmpty()
            .WithMessage(ResourceMessageException.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourceMessageException.EMAIL_INVALID);

        RuleFor(t => t.Password)
            .NotEmpty()
            .WithMessage(ResourceMessageException.PASSWORD_REQUIRED)
            .MinimumLength(6)
            .WithMessage(ResourceMessageException.PASSWORD_LENGTH)
            .Matches("[A-Z]").WithMessage(ResourceMessageException.PASSWORD_RULE_1)
            .Matches("[a-z]").WithMessage(ResourceMessageException.PASSWORD_RULE_2)
            .Matches("[0-9]").WithMessage(ResourceMessageException.PASSWORD_RULE_3);

        RuleFor(t => t.ConfirmPassword)
            .NotEmpty()
            .WithMessage(ResourceMessageException.CONFIRM_PASSWORD_REQUIRED)
            .Equal(t => t.Password)
            .WithMessage(ResourceMessageException.CONFIRM_PASSWORD_MATCH);

        RuleFor(t => t.Role)
            .NotEmpty()
            .WithMessage(ResourceMessageException.ROLE_REQUIRED)
            .Must(role =>
                role == "owner" ||
                role == "admin" ||
                role == "manager" ||
                role == "agent" ||
                role == "client" ||
                role == "financial" ||
                role == "assistant"
            )
            .WithMessage(ResourceMessageException.ROLE_MUST_BE);
    }
}

