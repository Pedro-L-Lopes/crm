using FluentMigrator;

namespace CRM.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.FIX_PLANHISTORY_FK_BEHAVIOR, "Adjust foreign key behaviors between Tenants and PlanHistories")]
public class Version0000004 : VersionBase
{
    public override void Up()
    {
        // 🔹 Remove antigas FKs
        Delete.ForeignKey("FK_PlanHistories_Tenants").OnTable("PlanHistories");
        Delete.ForeignKey("FK_Tenants_CurrentPlanHistory").OnTable("Tenants");

        // 🔹 Recria PlanHistories → Tenants com ON DELETE CASCADE
        Execute.Sql(@"
            ALTER TABLE PlanHistories
            ADD CONSTRAINT FK_PlanHistories_Tenants
            FOREIGN KEY (TenantId)
            REFERENCES Tenants(Id)
            ON DELETE CASCADE
            ON UPDATE CASCADE;
        ");

        // 🔹 Recria Tenants → PlanHistories com ON DELETE SET NULL
        Execute.Sql(@"
            ALTER TABLE Tenants
            ADD CONSTRAINT FK_Tenants_CurrentPlanHistory
            FOREIGN KEY (CurrentPlanHistoryId)
            REFERENCES PlanHistories(Id)
            ON DELETE SET NULL
            ON UPDATE CASCADE;
        ");
    }
}
