using CRM.Communication.Requests.Property;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.Property;

public class PropertyValidator : AbstractValidator<RequestPropertyJson>
{
    public PropertyValidator()
    {
        RuleFor(p => p.PropertyType)
            .NotEmpty()
            .WithMessage(ResourceMessageException.PROPERTY_TYPE_EMPTY);

        RuleFor(p => p.Purpose)
            .NotEmpty()
            .WithMessage(ResourceMessageException.PROPERTY_PURPOSE_EMPTY);

        RuleFor(p => p.Status)
            .NotEmpty()
            .WithMessage(ResourceMessageException.PROPERTY_STATUS_EMPTY);

        RuleFor(p => p.TenantId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.TENANT_REQUIRED);

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .When(p => p.Price.HasValue)
            .WithMessage(ResourceMessageException.PRICE_INVALID);
    }
}
