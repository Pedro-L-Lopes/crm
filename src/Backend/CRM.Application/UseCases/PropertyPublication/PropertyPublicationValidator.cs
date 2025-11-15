using CRM.Communication.Requests.PropertyPublication;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.PropertyPublication;

public class PropertyPublicationValidator : AbstractValidator<RequestPropertyPublication>
{
    public PropertyPublicationValidator()
    {
        RuleFor(p => p.TenantId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.TENANT_REQUIRED);

        RuleFor(p => p.PropertyId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.PROPERTY_ID_EMPTY);

        RuleFor(p => p.Platform)
            .NotEmpty()
            .WithMessage(ResourceMessageException.PLATFORM_EMPTY);

        RuleFor(p => p.Link)
            .NotEmpty()
            .WithMessage(ResourceMessageException.LINK_INVALID)
            .Must(link => Uri.TryCreate(link, UriKind.Absolute, out _))
            .WithMessage(ResourceMessageException.LINK_INVALID);
    }
}

