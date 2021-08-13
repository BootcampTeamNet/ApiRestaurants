using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration
{
    class DishCategoryConfiguration: IEntityTypeConfiguration<DishCategory>
    {
        public void Configure(EntityTypeBuilder<DishCategory> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(250);
            builder.Property(d => d.PathImage).HasMaxLength(256);
        }
    }
}
