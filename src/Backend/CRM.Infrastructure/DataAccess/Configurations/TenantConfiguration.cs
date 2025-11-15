using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.DataAccess.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.Property(t => t.Cycle)
            .HasConversion<string>();

        builder.HasOne(t => t.Plan)
               .WithMany()
               .HasForeignKey(t => t.PlanId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}