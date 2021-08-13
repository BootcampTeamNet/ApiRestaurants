using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration
{
    public class RestaurantCategoryConfiguration : IEntityTypeConfiguration<RestaurantCategory>
    {
        public void Configure(EntityTypeBuilder<RestaurantCategory> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(250);
            builder.Property(d => d.PathImage).HasMaxLength(256);
            builder.Property(r => r.IsActive);
        }
    }
}
