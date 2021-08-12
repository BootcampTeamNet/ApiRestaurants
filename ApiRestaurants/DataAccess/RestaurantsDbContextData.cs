using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RestaurantsDbContextData
    {
        public static async Task LoadDataAsync(RestaurantsDbContext context,
                                                ILoggerFactory loggerFactory)
        {
            try
            {

                if (!context.DishCategories.Any())
                {
                    var dishCategoriesData = File.ReadAllText("../DataAccess/LoadData/DishCategory.json");
                    var dishCategories = JsonSerializer.Deserialize<List<DishCategory>>(dishCategoriesData);

                    foreach (var category in dishCategories)
                    {
                        context.DishCategories.Add(category);
                    }

                    await context.SaveChangesAsync();
                }               

            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<RestaurantsDbContext>();
                logger.LogError(e.Message);
            }
        }


    }
}
