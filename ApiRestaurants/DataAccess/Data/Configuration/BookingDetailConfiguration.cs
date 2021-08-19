using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration
{
    public class BookingDetailConfiguration : IEntityTypeConfiguration<BookingDetail>
    {
        public void Configure(EntityTypeBuilder<BookingDetail> builder)
        {
            builder.Property(bd => bd.Quantity).IsRequired();
            builder.Property(bd => bd.Notes).HasMaxLength(1024);
            builder.HasOne(s => s.Booking).WithMany().HasForeignKey(bd => bd.BookingId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(d => d.Dish).WithMany().HasForeignKey(bd => bd.DishId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
