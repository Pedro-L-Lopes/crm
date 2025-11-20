using CRM.Communication.Requests.Client;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.Client.UpdateClient;

public class UpdateClientUseValidator : AbstractValidator<RequestClientJson>
{
    public UpdateClientUseValidator()
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
    }
}
