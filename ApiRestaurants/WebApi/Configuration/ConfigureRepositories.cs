using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Configuration
{
    public static class ConfigureRepositories
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IUserRestaurantRepository, UserRestaurantRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingStatusRepository, BookingStatusRepository>();
        }
    }
}
