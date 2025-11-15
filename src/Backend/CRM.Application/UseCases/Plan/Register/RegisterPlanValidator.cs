using CRM.Communication.Requests;
using FluentValidation;

namespace CRM.Application.UseCases.Plan.Register;

public class RegisterPlanValidator : AbstractValidator<RequestRegisterPlanJson>
{
    public RegisterPlanValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("O nome do plano é obrigatório.");
        RuleFor(p => p.Type).IsInEnum().WithMessage("O tipo do plano é inválido.");
        RuleFor(p => p.MonthlyPrice).GreaterThanOrEqualTo(0).WithMessage("O preço mensal não pode ser negativo.");
        RuleFor(p => p.MaxUsers).GreaterThan(0).WithMessage("O número de usuários deve ser pelo menos 1.");
    }
}
