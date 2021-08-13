using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class RestaurantCategoryRepository : IRestaurantCategoryRepository
    {
        private readonly RestaurantsDbContext _restaurantCategory;
        public RestaurantCategoryRepository(RestaurantsDbContext restaurantsDbContext)
        {
            _restaurantCategory = restaurantsDbContext;
        }

        public async Task<IReadOnlyList<string>> GetList()
        {
           List<string> restaurantCategoryData = new List<string>();
           restaurantCategoryData = await _restaurantCategory.Set<string>().ToListAsync();
            /*Set<id>().ToListAsync(rc => rc.Id == id);*/
            return restaurantCategoryData;
        }


  
    }
}
