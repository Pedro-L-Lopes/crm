using FluentMigrator;

namespace CRM.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.AddIndexesToFixTimeout, "Add indexes to fix timeout")]
public class Version0000006 : VersionBase
{
    public override void Up()
    {
        // Indexes for foreign keys
        Create.Index("idx_addresses_tenantid")
            .OnTable("Addresses")
            .OnColumn("TenantId");

        Create.Index("idx_clients_addressid")
            .OnTable("Clients")
            .OnColumn("AddressId");

        Create.Index("idx_clients_agentid")
            .OnTable("Clients")
            .OnColumn("AgentId");

        Create.Index("idx_clients_tenantid")
            .OnTable("Clients")
            .OnColumn("TenantId");

        // Unique index for Document
        Create.Index("idx_clients_document")
            .OnTable("Clients")
            .OnColumn("Document")
            .Unique();
    }
}
