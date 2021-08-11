using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(250);
            builder.Property(d => d.Description).IsRequired().HasMaxLength(1000);
            builder.Property(d => d.PathImage).HasMaxLength(256);
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)").HasDefaultValue(0.00);
            builder.Property(d => d.CaloriesMinimun).HasDefaultValue(0);
            builder.Property(d => d.CaloriesMaximun).HasDefaultValue(0);
            builder.Property(d => d.Proteins).HasDefaultValue(0);
            builder.Property(d => d.Fats).HasDefaultValue(0);
            builder.Property(d => d.Sugars).HasDefaultValue(0);
            builder.HasOne(c => c.DishCategory).WithMany().HasForeignKey(d => d.DishCategoryId);
        }
    }
}
