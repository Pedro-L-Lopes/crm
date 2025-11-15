using CRM.Communication.Requests;
using CRM.Domain.Entities;
using CRM.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.UseCases.PlanHistory.Register
{
    public class RegisterPlanHistoryValidator : AbstractValidator<RequestRegisterPlanHistoryJson>
    {
        public RegisterPlanHistoryValidator()
        {
            RuleFor(ph => ph.TenantId)
                .NotEmpty().WithMessage(ResourceMessageException.TENANT_REQUIRED);

            RuleFor(ph => ph.PlanId)
                .NotEmpty().WithMessage(ResourceMessageException.PLAN_ID_EMPTY);

            RuleFor(ph => ph.Cycle)
                .IsInEnum().WithMessage(ResourceMessageException.BILLING_CYCLE_INVALID);

            RuleFor(ph => ph.StartDate)
                .NotEmpty().WithMessage(ResourceMessageException.START_DATE_REQUIRED);

            RuleFor(ph => ph.EndDate)
                .NotEmpty().WithMessage(ResourceMessageException.END_DATE_REQUIRED)
                .GreaterThan(ph => ph.StartDate)
                .WithMessage(ResourceMessageException.END_DATE_AFTER_START_DATE);

            RuleFor(ph => ph.Status)
                .IsInEnum().WithMessage(ResourceMessageException.PLAN_STATUS_INVALID);

            RuleFor(ph => ph.PaymentStatus)
                .IsInEnum().WithMessage(ResourceMessageException.PAYMENT_STATUS_INVALID);

            RuleFor(ph => ph.PaymentMethod)
                .IsInEnum().WithMessage(ResourceMessageException.PAYMENT_METHOD_INVALID);

            RuleFor(ph => ph.AmountPaid)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessageException.AMOUNT_PAID_NEGATIVE);

            RuleFor(ph => ph.InvoiceUrl)
                .MaximumLength(255).WithMessage(ResourceMessageException.INVOICE_URL_MAX_LENGTH_EXCEEDED);
        }
    }
}
