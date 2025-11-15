using CRM.Domain.Enums;

namespace CRM.Communication.Responses;

public class ResponseRegisterPlanJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public PlanType Type { get; set; }

    public decimal MonthlyPrice { get; set; }

    public decimal AnnualPrice { get; set; }

    public int MaxUsers { get; set; }

    public int MaxProperties { get; set; }

    public bool IsActive { get; set; } = false;
}
