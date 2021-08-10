﻿using DataAccess.Implementations;
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
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        }
    }
}
