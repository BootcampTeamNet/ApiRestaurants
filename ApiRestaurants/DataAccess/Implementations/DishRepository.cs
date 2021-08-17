using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class DishRepository : IDishRepository
    {
        private readonly RestaurantsDbContext _restaurantsDbContext;
        public DishRepository(RestaurantsDbContext restaurantsDbContext)
        {
            _restaurantsDbContext = restaurantsDbContext;
        }

        public async Task<bool> ExistDish(int id)
        {
            var response = false;
            response = await _restaurantsDbContext.Set<Dish>().AnyAsync(x => x.Id == id);
            return response;

        }

        public async Task<List<Dish>> GetListByIdRestaurant(int id)
        {
            var dishesByRestaurant = await _restaurantsDbContext.Dishes.Where(i => i.RestaurantId == id).ToListAsync();
            return dishesByRestaurant;
        }

        public async Task<List<Dish>> GetActiveDishList(int id)
        {
            var activeDishes = await _restaurantsDbContext.Dishes.Where(i => i.RestaurantId == id && i.IsActive).ToListAsync();
            return activeDishes;
        }
    }
}