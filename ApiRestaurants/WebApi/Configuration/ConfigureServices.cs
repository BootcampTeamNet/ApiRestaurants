using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Implementations.Dishes;
using Services.Implementations.Shared;
using Services.Inplementations.Users;
using Services.Interfaces;
using Services.Implementations.Restaurants;

namespace WebApi.Configuration
{
    public static class ConfigureServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IDishCategoryService, DishCategoryService>();
            services.AddScoped<IRestaurantCategoryService, RestaurantCategoryService>();
            services.AddScoped<IBranchOfficeService, BranchOfficeService>();
        }
    }
}
