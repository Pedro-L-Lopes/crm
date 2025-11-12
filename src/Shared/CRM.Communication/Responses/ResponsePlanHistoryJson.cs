namespace CRM.Communication.Responses
{
    public class ResponsePlanHistoryJson
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid PlanId { get; set; }
        public string Cycle { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public string InvoiceUrl { get; set; } = string.Empty;

        public PlanInfo? Plan { get; set; }

        public class PlanInfo
        {
            public string Name { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public decimal MonthlyPrice { get; set; }
            public decimal AnnualPrice { get; set; }
        }
    }
}
