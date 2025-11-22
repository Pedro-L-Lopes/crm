using CRM.Communication.Requests.Address;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.Address.Register;

public class RegisterAddressValidator : AbstractValidator<RequestAddressJson>
{
    public RegisterAddressValidator()
    {
        RuleFor(a => a.TenantId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.INVALID_TENANT);

        RuleFor(a => a.ZipCode)
            .NotEmpty()
            .WithMessage(ResourceMessageException.ZIPCODE_EMPTY);

        RuleFor(a => a.Street)
            .NotEmpty()
            .WithMessage(ResourceMessageException.STREET_EMPTY);
            
        RuleFor(a => a.Neighborhood)
            .NotEmpty()
            .WithMessage(ResourceMessageException.STREET_EMPTY);

        RuleFor(a => a.City)
            .NotEmpty()
            .WithMessage(ResourceMessageException.CITY_EMPTY);

        RuleFor(a => a.State)
            .NotEmpty()
            .WithMessage(ResourceMessageException.STATE_EMPTY);
    }
}
