namespace CRM.Infrastructure.Migrations;

public abstract class DatabaseVersions
{
    public const int TABLE_TENANT_USER_AND_PLAN = 1;
    public const int ADD_PLAN_HISTORY_AND_PAYMENTS = 2;
    public const int ADD_PLAN_HISTORY_AND_PAYMENTS2 = 3;
    public const int FIX_PLANHISTORY_FK_BEHAVIOR = 4;
}
