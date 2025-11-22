using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace CRM.Infrastructure.Migrations.Versions;
public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTabel(string table)
    {
        return Create.Table(table)
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false)
            .WithColumn("CreatedAt").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("UpdatedAt").AsDateTime().WithDefault(SystemMethods.CurrentDateTime);
    }
}
