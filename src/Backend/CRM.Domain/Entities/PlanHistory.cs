using CRM.Domain.Enums;
using System;

namespace CRM.Domain.Entities
{
    public class PlanHistory : EntityBase
    {
        public Guid TenantId { get; set; }
        public Guid PlanId { get; set; }
        public BillingCycle Cycle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PlanStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public string InvoiceUrl { get; set; } = string.Empty;

        public Tenant Tenant { get; set; }
        public Plan Plan { get; set; }
    }
}
