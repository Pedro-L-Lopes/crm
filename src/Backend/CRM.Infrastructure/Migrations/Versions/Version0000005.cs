using FluentMigrator;

namespace CRM.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_CLIENTS_PROPERTIES_ADDRESSES, "Create table clients and properties")]
public class Version0000005 : VersionBase
{
    public override void Up()
    {
        CreateTabel("Addresses")
            .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_Addresses_Tenants_Id", "Tenants", "Id")
                .OnDelete(System.Data.Rule.Cascade)
                .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("ZipCode").AsString(20).Nullable()
            .WithColumn("Street").AsString(100).Nullable()
            .WithColumn("Number").AsString(20).Nullable()
            .WithColumn("Complement").AsString(100).Nullable()
            .WithColumn("Neighborhood").AsString(100).Nullable()
            .WithColumn("City").AsString(100).Nullable()
            .WithColumn("State").AsString(50).Nullable()
            .WithColumn("Latitude").AsDecimal(10, 8).Nullable()
            .WithColumn("Longitude").AsDecimal(11, 8).Nullable();

        Create.Index("IX_Addresses_TenantId")
            .OnTable("Addresses")
            .OnColumn("TenantId");

        Create.Index("IX_Addresses_City")
            .OnTable("Addresses")
            .OnColumn("City");

        Create.Index("IX_Addresses_State")
            .OnTable("Addresses")
            .OnColumn("State");

        Create.Index("IX_Addresses_ZipCode")
            .OnTable("Addresses")
            .OnColumn("ZipCode");

        CreateTabel("Clients")
            .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_Clients_Tenants_Id", "Tenants", "Id")
                    .OnDelete(System.Data.Rule.Cascade)
                    .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("AgentId").AsGuid().NotNullable().ForeignKey("FK_Clients_Users_Id", "Users", "Id")
            .WithColumn("AddressId").AsGuid().Nullable().ForeignKey("FK_Clients_Addresses_Id", "Addresses", "Id")
                    .OnDelete(System.Data.Rule.SetNull)
                    .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Email").AsString(50).Nullable()
            .WithColumn("Phone").AsString(15).Nullable()
            .WithColumn("Document").AsString(20).NotNullable().Unique()
            .WithColumn("SecondDocument").AsString(20).Nullable()
            .WithColumn("Type").AsString(20).NotNullable().WithDefaultValue("individual") // individual, company
            .WithColumn("Notes").AsCustom("TEXT").Nullable()
            .WithColumn("BirthDate").AsDate().Nullable()
            .WithColumn("Occupation").AsString(50).Nullable()
            .WithColumn("Income").AsDecimal(12, 2).Nullable()
            .WithColumn("Gender").AsString(10).Nullable(); // male, female, other

        Create.Index("IX_Clients_TenantId")
            .OnTable("Clients")
            .OnColumn("TenantId");

        Create.Index("IX_Clients_IsDeleted")
            .OnTable("Clients")
            .OnColumn("IsDeleted");

        Create.Index("UQ_Clients_Document")
            .OnTable("Clients")
            .OnColumn("Document")
            .Unique();

        Create.Index("IX_Clients_AddressId")
              .OnTable("Clients")
              .OnColumn("AddressId");

        CreateTabel("Properties")
            .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_Properties_Tenants_Id", "Tenants", "Id")
                    .OnDelete(System.Data.Rule.Cascade)
                    .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("OwnerId").AsGuid().Nullable().ForeignKey("FK_Properties_Clients_Id", "Clients", "Id")
            .WithColumn("AddressId").AsGuid().Nullable().ForeignKey("FK_Properties_Addresses_Id", "Addresses", "Id")
                    .OnDelete(System.Data.Rule.SetNull)
                    .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("Code").AsInt32().NotNullable()
            .WithColumn("Description").AsCustom("TEXT").Nullable()
            .WithColumn("PropertyType").AsString(30).NotNullable() // house, apartment, land, commercial, farm, studio, duplex, warehouse, shop
            .WithColumn("Purpose").AsString(20).NotNullable().WithDefaultValue("sale") // sale, rent, seasonal
            .WithColumn("Status").AsString(20).NotNullable().WithDefaultValue("available") // available, rented, sold, moderation
            .WithColumn("CanBeFinanced").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("Price").AsDecimal(12, 2).Nullable()
            .WithColumn("LastActivity").AsDate().Nullable()
            .WithColumn("TotalArea").AsDecimal(10, 2).Nullable()
            .WithColumn("BuiltArea").AsDecimal(10, 2).Nullable()
            .WithColumn("Bedrooms").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("Bathrooms").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("Suites").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("ParkingSpaces").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("YearBuilt").AsInt32().Nullable();

        Create.Index("IX_Properties_TenantId")
            .OnTable("Properties")
            .OnColumn("TenantId");

        Create.Index("IX_Properties_OwnerId")
            .OnTable("Properties")
            .OnColumn("OwnerId");

        Create.Index("IX_Properties_Code")
            .OnTable("Properties")
            .OnColumn("Code")
            .Unique();

        Create.Index("IX_Properties_AddressId")
            .OnTable("Properties")
            .OnColumn("AddressId");

        Create.Index("IX_Properties_Status")
            .OnTable("Properties")
            .OnColumn("Status");

        Create.Index("IX_Properties_IsDeleted")
            .OnTable("Properties")
            .OnColumn("IsDeleted");

        Create.Index("IX_Properties_PropertyType")
            .OnTable("Properties")
            .OnColumn("PropertyType");

        Create.Index("IX_Properties_Purpose")
            .OnTable("Properties")
            .OnColumn("Purpose");

        CreateTabel("PropertyPublications")
            .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_PropertyPublications_Tenants_Id", "Tenants", "Id")
                .OnDelete(System.Data.Rule.Cascade)
                .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("PropertyId").AsGuid().NotNullable().ForeignKey("FK_PropertyPublications_Properties_Id", "Properties", "Id")
                .OnDelete(System.Data.Rule.Cascade)
                .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("Platform").AsString(20).NotNullable() // instagram, facebook, tiktok, youtube, linkedin
            .WithColumn("Link").AsString(255).NotNullable()
            .WithColumn("Caption").AsCustom("TEXT").Nullable()
            .WithColumn("PostDate").AsDate().Nullable();

        Create.Index("IX_PropertyPublications_PropertyId")
            .OnTable("PropertyPublications")
            .OnColumn("PropertyId");

        Create.Index("IX_PropertyPublications_Platform")
            .OnTable("PropertyPublications")
            .OnColumn("Platform");

        Create.Index("IX_PropertyPublications_IsDeleted")
            .OnTable("PropertyPublications")
            .OnColumn("IsDeleted");

        CreateTabel("PropertyVisits")
            .WithColumn("TenantId").AsGuid().NotNullable().ForeignKey("FK_PropertyVisits_Tenants_Id", "Tenants", "Id")
                .OnDelete(System.Data.Rule.Cascade)
                .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("PropertyId").AsGuid().NotNullable().ForeignKey("FK_PropertyVisits_Properties_Id", "Properties", "Id")
                .OnDelete(System.Data.Rule.Cascade)
                .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("ClientId").AsGuid().NotNullable().ForeignKey("FK_PropertyVisits_Clients_Id", "Clients", "Id")
                .OnDelete(System.Data.Rule.Cascade)
                .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("AgentId").AsGuid().NotNullable().ForeignKey("FK_PropertyVisits_Users_Id", "Users", "Id")
                .OnDelete(System.Data.Rule.Cascade)
                .OnUpdate(System.Data.Rule.Cascade)
            .WithColumn("ScheduledAt").AsDateTime().NotNullable()
            .WithColumn("Status").AsString(20).NotNullable().WithDefaultValue("scheduled") // scheduled, completed, cancelled, no_show
            .WithColumn("Notes").AsCustom("TEXT").Nullable()
            .WithColumn("SummaryCopied").AsBoolean().NotNullable().WithDefaultValue(false);

        Create.Index("IX_PropertyVisits_TenantId")
            .OnTable("PropertyVisits")
            .OnColumn("TenantId");

        Create.Index("IX_PropertyVisits_PropertyId")
            .OnTable("PropertyVisits")
            .OnColumn("PropertyId");

        Create.Index("IX_PropertyVisits_ClientId")
            .OnTable("PropertyVisits")
            .OnColumn("ClientId");

        Create.Index("IX_PropertyVisits_AgentId")
            .OnTable("PropertyVisits")
            .OnColumn("AgentId");

        Create.Index("IX_PropertyVisits_Status")
            .OnTable("PropertyVisits")
            .OnColumn("Status");

        Create.Index("IX_PropertyVisits_ScheduledAt")
            .OnTable("PropertyVisits")
            .OnColumn("ScheduledAt");

        Create.Index("IX_PropertyVisits_IsDeleted")
            .OnTable("PropertyVisits")
            .OnColumn("IsDeleted");

        

    }
}
