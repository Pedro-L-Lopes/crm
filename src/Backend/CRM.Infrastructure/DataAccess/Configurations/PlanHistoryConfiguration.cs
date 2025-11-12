using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.DataAccess.Configurations;

public class PlanHistoryConfiguration : IEntityTypeConfiguration<PlanHistory>
{
    public void Configure(EntityTypeBuilder<PlanHistory> builder)
    {
        builder.Property(p => p.Cycle)
               .HasConversion<string>();

        builder.Property(p => p.Status)
               .HasConversion<string>();

        builder.Property(p => p.PaymentStatus)
               .HasConversion<string>();

        builder.Property(p => p.PaymentMethod)
               .HasConversion<string>();
    }
}
