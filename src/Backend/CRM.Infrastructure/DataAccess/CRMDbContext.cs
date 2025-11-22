using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.DataAccess;
public class CRMDbContext : DbContext
{
    public CRMDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<PlanHistory> PlanHistories { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyPublication> PropertyPublications { get; set; }
    public DbSet<PropertyVisit> PropertyVisits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CRMDbContext).Assembly);
    }
}
