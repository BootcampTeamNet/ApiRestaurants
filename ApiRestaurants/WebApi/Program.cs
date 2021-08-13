using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var logger1 = loggerFactory.CreateLogger<Program>();
                    logger1.LogError("Creando migraciones");
                    var context = services.GetRequiredService<RestaurantsDbContext>();
                    await context.Database.MigrateAsync();

                    await RestaurantsDbContextData.LoadDataAsync(context, loggerFactory);

                }
                catch (Exception e)
                {
                    var logger2 = loggerFactory.CreateLogger<Program>();

                    logger2.LogError(e, "Errores en el proceso de migración...");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}