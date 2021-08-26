using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Implementations.Bookings;
using Services.Implementations.Dishes;
using Services.Implementations.Favourites;
using Services.Implementations.Restaurants;
using Services.Implementations.Shared;
using Services.Inplementations.Users;
using Services.Interfaces;

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
            services.AddScoped<IUserRestaurantService, UserRestaurantService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IStringProcess, StringProcess>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingStatusService, BookingStatusService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IFavouriteService, FavouriteService>();
        }
    }
}
