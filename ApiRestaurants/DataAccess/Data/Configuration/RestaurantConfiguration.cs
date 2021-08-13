using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Address).IsRequired().HasMaxLength(256);
            builder.Property(r => r.LocationLatitude).HasMaxLength(256);
            builder.Property(r => r.LocationLongitude).HasMaxLength(256);
            builder.Property(r => r.TimeMaxCancelBooking);
            builder.Property(r => r.ScheduleFrom);
            builder.Property(r => r.ScheduleTo);
            builder.Property(r => r.Phone).HasMaxLength(20);
            builder.Property(r => r.PathImage).HasMaxLength(256);
            builder.Property(r => r.MainBranchId);
            builder.Property(r => r.Email).HasMaxLength(100);
            builder.Property(r => r.IsActive);
            builder.HasOne(c => c.RestaurantCategory).WithMany().HasForeignKey(r => r.RestaurantCategoryId);
        }
    }
}
