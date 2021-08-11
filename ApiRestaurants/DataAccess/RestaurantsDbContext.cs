using Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess
{
    public class RestaurantsDbContext : DbContext
    {
        public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : base(options) { }
        public DbSet<Dishe> Dishes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<UserRestaurant> UserRestaurants { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}