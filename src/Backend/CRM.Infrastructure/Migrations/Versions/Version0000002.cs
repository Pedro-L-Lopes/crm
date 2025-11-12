using FluentMigrator;

namespace CRM.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.ADD_PLAN_HISTORY_AND_PAYMENTS, "Add missing fields and new columns to existing tables")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        // 🔧 Exemplo: adicionar campos ausentes nas tabelas existentes

        // TABLE: Tenants
        if (!Schema.Table("Tenants").Column("Address").Exists())
        {
            Alter.Table("Tenants")
                .AddColumn("Address").AsString(255).Nullable();
        }

        if (!Schema.Table("Tenants").Column("Document").Exists())
        {
            Alter.Table("Tenants")
                .AddColumn("Document").AsString(20).Nullable();
        }

        if (!Schema.Table("Tenants").Column("EntityType").Exists())
        {
            Alter.Table("Tenants")
                .AddColumn("EntityType").AsString(20).Nullable();
        }

        // TABLE: Users
        if (!Schema.Table("Users").Column("Gender").Exists())
        {
            Alter.Table("Users")
                .AddColumn("Gender").AsString(1).Nullable();
        }

        if (!Schema.Table("Users").Column("TwoFactorEnabled").Exists())
        {
            Alter.Table("Users")
                .AddColumn("TwoFactorEnabled").AsBoolean().WithDefaultValue(false);
        }

        // TABLE: Plans
        if (!Schema.Table("Plans").Column("Description").Exists())
        {
            Alter.Table("Plans")
                .AddColumn("Description").AsString(255).Nullable();
        }

        // ✅ Exemplo de novo índice (caso precise otimizar consultas)
        if (!Schema.Table("Users").Index("IX_Users_Email_Active").Exists())
        {
            Create.Index("IX_Users_Email_Active")
                .OnTable("Users")
                .OnColumn("Email").Ascending()
                .OnColumn("IsActive").Ascending();
        }
    }
}
