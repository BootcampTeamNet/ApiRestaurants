using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(b => b.OrderDate).IsRequired();
            builder.Property(b => b.NumberPeople).IsRequired();
            builder.Property(b => b.BookingNumber).HasMaxLength(256);
            builder.HasOne(s => s.BookingStatus).WithMany().HasForeignKey(b => b.BookingStatusId);
            builder.HasOne(u => u.User).WithMany().HasForeignKey(b => b.UserId);
            builder.HasOne(r => r.Restaurant).WithMany().HasForeignKey(b => b.RestaurantId);
        }
    }
}
