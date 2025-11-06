using FluentMigrator;

namespace CRM.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_TENANT_AND_USER, "Create tables to save tenants and users")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        // TENANTS
        CreateTabel("Tenants")
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("SubDomain").AsString(50).Nullable()
            .WithColumn("Email").AsString(70).Nullable()
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
        Create.Index("IX_Tenants_SubDomain").OnTable("Tenants").OnColumn("SubDomain");
        Create.Index("IX_Tenants_IsDeleted").OnTable("Tenants").OnColumn("IsDeleted");

        Create.Index("IX_Users_Tenant").OnTable("Users").OnColumn("TenantId");
        Create.Index("IX_Users_Email").OnTable("Users").OnColumn("Email");
        Create.Index("IX_Users_IsDeleted").OnTable("Users").OnColumn("IsDeleted");
    }
}
