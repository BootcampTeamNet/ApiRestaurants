using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configuration
{
    public class UserRestaurantConfiguration : IEntityTypeConfiguration<UserRestaurant>
    {
        public void Configure(EntityTypeBuilder<UserRestaurant> builder)
        {
            builder.Property(ur => ur.UserId).IsRequired();
            builder.Property(ur => ur.RestaurantId).IsRequired();
        }
    }
}
