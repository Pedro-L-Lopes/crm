using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Infrastructure.DataAccess.Configurations
{
    internal class PropertyConfigurations : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(p => p.PropertyType)
                   .HasConversion<string>();

            builder.Property(p => p.Purpose)
                   .HasConversion<string>();

            builder.Property(p => p.Status)
                   .HasConversion<string>();
        }
    }
}
