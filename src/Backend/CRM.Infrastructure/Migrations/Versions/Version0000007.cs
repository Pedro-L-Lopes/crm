using FluentMigrator;

namespace CRM.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.AddCascadeDeleteFix, "Add cascade delete for Clients, Addresses and Properties")]
public class Version0000007 : VersionBase
{
    public override void Up()
    {
        //
        // CLIENTS
        //

        Alter.Column("AgentId")
            .OnTable("clients")
            .AsGuid()
            .Nullable();


        // Clients → Addresses
        Delete.ForeignKey("FK_Clients_Addresses_Id").OnTable("clients");
        Create.ForeignKey("FK_Clients_Addresses_Id")
            .FromTable("clients").ForeignColumn("AddressId")
            .ToTable("addresses").PrimaryColumn("Id")
            .OnDeleteOrUpdate(System.Data.Rule.Cascade);


        // Clients → Tenants
        Delete.ForeignKey("FK_Clients_Tenants_Id").OnTable("clients");
        Create.ForeignKey("FK_Clients_Tenants_Id")
            .FromTable("clients").ForeignColumn("TenantId")
            .ToTable("tenants").PrimaryColumn("Id")
            .OnDeleteOrUpdate(System.Data.Rule.Cascade);


        //
        // ADDRESSES
        //

        Delete.ForeignKey("FK_Addresses_Tenants_Id").OnTable("addresses");
        Create.ForeignKey("FK_Addresses_Tenants_Id")
            .FromTable("addresses").ForeignColumn("TenantId")
            .ToTable("tenants").PrimaryColumn("Id")
            .OnDeleteOrUpdate(System.Data.Rule.Cascade);


        //
        // PROPERTIES
        //

        Delete.ForeignKey("FK_Properties_Addresses_Id").OnTable("properties");
        Create.ForeignKey("FK_Properties_Addresses_Id")
            .FromTable("properties").ForeignColumn("AddressId")
            .ToTable("addresses").PrimaryColumn("Id")
            .OnDeleteOrUpdate(System.Data.Rule.Cascade);

        Delete.ForeignKey("FK_Properties_Clients_Id").OnTable("properties");
        Create.ForeignKey("FK_Properties_Clients_Id")
            .FromTable("properties").ForeignColumn("OwnerId")
            .ToTable("clients").PrimaryColumn("Id")
            .OnDeleteOrUpdate(System.Data.Rule.Cascade);

        Delete.ForeignKey("FK_Properties_Tenants_Id").OnTable("properties");
        Create.ForeignKey("FK_Properties_Tenants_Id")
            .FromTable("properties").ForeignColumn("TenantId")
            .ToTable("tenants").PrimaryColumn("Id")
            .OnDeleteOrUpdate(System.Data.Rule.Cascade);
    }
}
