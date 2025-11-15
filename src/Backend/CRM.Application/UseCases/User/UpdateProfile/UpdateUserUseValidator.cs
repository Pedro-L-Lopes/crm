using CRM.Communication.Requests;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.User.UpdateProfile;

public class UpdateUserUseValidator : AbstractValidator<RequestUpdateUserProfileJson>
{
    public UpdateUserUseValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);
        RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessageException.EMAIL_EMPTY);
        RuleFor(request => request.Phone)
            .NotEmpty().WithMessage(ResourceMessageException.PHONE_EMPTY)
            .Matches(@"^\d{10,15}$").WithMessage(ResourceMessageException.PHONE_INVALID);
        RuleFor(request => request.Gender)
            .NotEmpty().WithMessage(ResourceMessageException.GENDER_EMPTY)
            .Matches("^[MF]$").WithMessage(ResourceMessageException.GENDER_INVALID);

        When(request => request.Email.Length > 1, () =>
        {
            RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMessageException.EMAIL_INVALID);
        });
    }
}
