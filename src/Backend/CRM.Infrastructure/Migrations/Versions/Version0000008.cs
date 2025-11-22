using FluentMigrator;

namespace CRM.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.AddCascadeDeleteFixV2, "Fix cascade delete for client/property addresses")]
public class Version0000008 : ForwardOnlyMigration
{
    public override void Up()
    {
        //
        // CLIENTS → ADDRESSES
        //
        Delete.ForeignKey("FK_Clients_Addresses_Id").OnTable("Clients");
        Create.ForeignKey("FK_Clients_Addresses_Id")
            .FromTable("Clients").ForeignColumn("AddressId")
            .ToTable("Addresses").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade)     // APAGA ADDRESS JUNTO
            .OnUpdate(System.Data.Rule.Cascade);

        //
        // PROPERTIES → ADDRESSES
        //
        Delete.ForeignKey("FK_Properties_Addresses_Id").OnTable("Properties");
        Create.ForeignKey("FK_Properties_Addresses_Id")
            .FromTable("Properties").ForeignColumn("AddressId")
            .ToTable("Addresses").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade)     // APAGA ADDRESS JUNTO
            .OnUpdate(System.Data.Rule.Cascade);

        //
        // CLIENTS → TENANTS
        //
        Delete.ForeignKey("FK_Clients_Tenants_Id").OnTable("Clients");
        Create.ForeignKey("FK_Clients_Tenants_Id")
            .FromTable("Clients").ForeignColumn("TenantId")
            .ToTable("Tenants").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade)
            .OnUpdate(System.Data.Rule.Cascade);

        //
        // PROPERTIES → TENANTS
        //
        Delete.ForeignKey("FK_Properties_Tenants_Id").OnTable("Properties");
        Create.ForeignKey("FK_Properties_Tenants_Id")
            .FromTable("Properties").ForeignColumn("TenantId")
            .ToTable("Tenants").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.Cascade)
            .OnUpdate(System.Data.Rule.Cascade);

        //
        // CLIENTS → USERS (Agent)
        //
        Alter.Column("AgentId").OnTable("Clients").AsGuid().Nullable(); // necessário para SET NULL

        Create.ForeignKey("FK_Clients_Users_Id")
            .FromTable("Clients").ForeignColumn("AgentId")
            .ToTable("Users").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.SetNull)      // não pode ser cascade
            .OnUpdate(System.Data.Rule.Cascade);

        //
        // PROPERTIES → CLIENTS
        //
        Delete.ForeignKey("FK_Properties_Clients_Id").OnTable("Properties");
        Create.ForeignKey("FK_Properties_Clients_Id")
            .FromTable("Properties").ForeignColumn("OwnerId")
            .ToTable("Clients").PrimaryColumn("Id")
            .OnDelete(System.Data.Rule.SetNull)      // recomendado: não apagar imóvel ao apagar cliente
            .OnUpdate(System.Data.Rule.Cascade);
    }
}
