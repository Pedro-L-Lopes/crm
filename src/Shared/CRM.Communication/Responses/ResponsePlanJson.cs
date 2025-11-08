using CRM.Domain.Enums;

namespace CRM.Communication.Responses;

public class ResponsePlanJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public PlanType Type { get; set; }

    public decimal MonthlyPrice { get; set; }

    public decimal AnnualPrice { get; set; }

    public int MaxUsers { get; set; }

    public int MaxProperties { get; set; }

    public int MaxStorageMb { get; set; }

    public bool CanExportData { get; set; }

    public bool HasWhatsappAutomation { get; set; }

    public bool HasDigitalSignature { get; set; }

    public bool HasSupportPriority { get; set; }
    public bool IsActive { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
