using FluentMigrator;

namespace CRM.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.ADD_PLAN_HISTORY_AND_PAYMENTS2, "Add tables for plan history and payments, and update Tenants 2")]
public class Version0000003 : VersionBase
{
    public override void Up()
    {
        // ================================
        // 🧾 PLAN HISTORIES
        // ================================
        CreateTabel("PlanHistories")
            .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_PlanHistories_Tenants", "Tenants", "Id")
            .WithColumn("PlanId").AsGuid().NotNullable().ForeignKey("FK_PlanHistories_Plans", "Plans", "Id")
            .WithColumn("Cycle").AsString(20).NotNullable()
            .WithColumn("StartDate").AsDateTime().NotNullable()
            .WithColumn("EndDate").AsDateTime().Nullable()
            .WithColumn("Status").AsString(20).NotNullable() // Active, Pending, Expired, Cancelled
            .WithColumn("PaymentStatus").AsString(20).NotNullable() // Paid, Unpaid, Failed
            .WithColumn("PaymentMethod").AsString(30).Nullable() // Pix, CreditCard, Boleto
            .WithColumn("AmountPaid").AsDecimal(10, 2).WithDefaultValue(0.00)
            .WithColumn("InvoiceUrl").AsString(255).Nullable();

        // ================================
        // 💰 PAYMENTS
        // ================================
        CreateTabel("Payments")
            .WithColumn("PlanHistoryId").AsGuid().NotNullable().ForeignKey("FK_Payments_PlanHistories", "PlanHistories", "Id")
            .WithColumn("TransactionId").AsString(100).Nullable()
            .WithColumn("Amount").AsDecimal(10, 2).NotNullable()
            .WithColumn("PaymentMethod").AsString(30).Nullable()
            .WithColumn("PaymentDate").AsDateTime().Nullable()
            .WithColumn("Status").AsString(20).NotNullable() // Paid, Pending, Failed, Refunded
            .WithColumn("GatewayResponse").AsString(500).Nullable();

        // ================================
        // 🏢 ALTER TENANTS
        // ================================
        Alter.Table("Tenants")
            .AddColumn("CurrentPlanHistoryId").AsGuid().Nullable();

        // Foreign Key (tenant -> plan history)
        Create.ForeignKey("FK_Tenants_CurrentPlanHistory")
            .FromTable("Tenants").ForeignColumn("CurrentPlanHistoryId")
            .ToTable("PlanHistories").PrimaryColumn("Id");

        // Index
        Create.Index("IX_Tenants_CurrentPlanHistory").OnTable("Tenants").OnColumn("CurrentPlanHistoryId");

        // ================================
        // 📇 INDEXES
        // ================================
        Create.Index("IX_PlanHistories_Tenant").OnTable("PlanHistories").OnColumn("TenantId");
        Create.Index("IX_PlanHistories_Plan").OnTable("PlanHistories").OnColumn("PlanId");
        Create.Index("IX_Payments_PlanHistory").OnTable("Payments").OnColumn("PlanHistoryId");
    }
}
