using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration
{
    public class DisheConfiguration : IEntityTypeConfiguration<Dishe>
    {
        public void Configure(EntityTypeBuilder<Dishe> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(250);
            builder.Property(d => d.Ingredients).IsRequired().HasMaxLength(1000);
            builder.Property(d => d.PathImage).HasMaxLength(256);
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)");
        }
    }
}
