using FluentMigrator;

namespace CRM.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_TENANT_USER_AND_PLAN, "Create tables to save tenants, users and plans")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        // PLANS
        CreateTabel("Plans")
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Type").AsString(20).NotNullable() // "free", "trial", "premium", "enterprise"
                .WithColumn("MonthlyPrice").AsDecimal(10, 2).WithDefaultValue(0.00)
                .WithColumn("AnnualPrice").AsDecimal(10, 2).WithDefaultValue(0.00)
                .WithColumn("MaxUsers").AsInt32().WithDefaultValue(5)
                .WithColumn("MaxProperties").AsInt32().WithDefaultValue(100)
                .WithColumn("MaxStorageMb").AsInt32().WithDefaultValue(500)
                .WithColumn("CanExportData").AsBoolean().WithDefaultValue(true)
                .WithColumn("HasWhatsappAutomation").AsBoolean().WithDefaultValue(false)
                .WithColumn("HasDigitalSignature").AsBoolean().WithDefaultValue(false)
                .WithColumn("HasSupportPriority").AsBoolean().WithDefaultValue(false)
                .WithColumn("IsActive").AsBoolean().Nullable();

        // TENANTS
        CreateTabel("Tenants")
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("SubDomain").AsString(50).Nullable()
            .WithColumn("Email").AsString(70).Nullable()
            .WithColumn("Cycle").AsString(20)
            .WithColumn("Phone").AsString(20).Nullable()
            .WithColumn("PlanId").AsGuid().Nullable()
            .WithColumn("PlanExpiration").AsDate().Nullable()
            .WithColumn("Type").AsString(20).NotNullable() // individual, agent, agency
            .WithColumn("IsActive").AsBoolean().Nullable();

        // USERS
        CreateTabel("Users")
            .WithColumn("TenantId").AsGuid().Nullable().ForeignKey("FK_Users_Tenants", "Tenants", "Id")
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable()
            .WithColumn("Password").AsString(255).NotNullable()
            .WithColumn("Role").AsString(20).WithDefaultValue("assistant") // owner, admin, manager, etc.
            .WithColumn("LastLogin").AsDateTime().Nullable()
            .WithColumn("IsActive").AsBoolean().WithDefaultValue(true)
            .WithColumn("Phone").AsString(14).Nullable();

        // INDEXES
        Create.Index("IX_Plans_IsDeleted").OnTable("Plans").OnColumn("IsDeleted");

        // 🔧 Novo índice não único (para consultas rápidas por tipo)
        Create.Index("IX_Plans_Type").OnTable("Plans").OnColumn("Type");

        Create.Index("IX_Tenants_SubDomain").OnTable("Tenants").OnColumn("SubDomain");
        Create.Index("IX_Tenants_Plan").OnTable("Tenants").OnColumn("PlanId");
        Create.Index("IX_Tenants_IsDeleted").OnTable("Tenants").OnColumn("IsDeleted");

        Create.Index("IX_Users_Tenant").OnTable("Users").OnColumn("TenantId");
        Create.Index("IX_Users_Email").OnTable("Users").OnColumn("Email");
        Create.Index("IX_Users_IsDeleted").OnTable("Users").OnColumn("IsDeleted");
    }
}
