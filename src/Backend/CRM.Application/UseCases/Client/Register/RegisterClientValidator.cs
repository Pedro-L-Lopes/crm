using CRM.Communication.Requests.Client;
using CRM.Domain.Enums;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.Client.Register;

public class RegisterClientValidator : AbstractValidator<RequestClientJson>
{
    public RegisterClientValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ResourceMessageException.NAME_EMPTY)
            .MinimumLength(3)
            .WithMessage(ResourceMessageException.NAME_LENGTH);

        RuleFor(c => c.Document)
            .NotEmpty()
            .WithMessage(ResourceMessageException.DOCUMENT_EMPTY);

        RuleFor(c => c.Email)
            .EmailAddress()
            .When(c => !string.IsNullOrWhiteSpace(c.Email))
            .WithMessage(ResourceMessageException.EMAIL_INVALID);

        RuleFor(c => c.Phone)
            .MinimumLength(8)
            .When(c => !string.IsNullOrWhiteSpace(c.Phone))
            .WithMessage(ResourceMessageException.PHONE_INVALID);

        RuleFor(c => c.Type)
            .NotEmpty()
            .WithMessage(ResourceMessageException.CLIENT_TYPE_EMPTY);
            //.IsInEnum()
            //.WithMessage(ResourceMessageException.CLIENT_TYPE_EMPTY);

        RuleFor(c => c.TenantId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.TENANT_REQUIRED);

        RuleFor(c => c.AgentId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.AGENT_EMPTY);
    }
}
