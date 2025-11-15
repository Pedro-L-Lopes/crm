namespace CRM.Communication.Responses;

public class ResponseUserProfileJson
{
    // 🧍 Dados do usuário
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime LastLogin { get; set; }

    // 🏢 Tenant (empresa)
    public TenantInfo Tenant { get; set; } = new();

    // 💳 Plano
    public PlanInfo Plan { get; set; } = new();

    public class TenantInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SubDomain { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid PlanId { get; set; }
        public DateTime? PlanExpiration { get; set; }
    }

    public class PlanInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal MonthlyPrice { get; set; }
        public decimal AnnualPrice { get; set; }
        public int MaxUsers { get; set; }
        public int MaxProperties { get; set; }
        public int MaxStorageMb { get; set; }
        public bool CanExportData { get; set; }
        public bool HasWhatsappAutomation { get; set; }
        public bool HasDigitalSignature { get; set; }
        public bool HasSupportPriority { get; set; }
        public bool IsActive { get; set; }
    }
}
