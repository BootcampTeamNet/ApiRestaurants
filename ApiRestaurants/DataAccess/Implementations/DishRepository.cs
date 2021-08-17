using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Dish> GetDish(string name)
        {
            var dish = new Dish();
            dish = await _restaurantsDbContext.Dishes.FirstOrDefaultAsync(
                x => x.Name.Equals(name.ToLower()));
            return dish;
        }
        public async Task<Dish> GetById(int id)
        {
            Dish dishData = new Dish();
            dishData = await _restaurantsDbContext.Dishes.FirstOrDefaultAsync(d => d.Id == id);
            return dishData;
        }

        public async Task<Dish> GetListByIdRestaurant(int id)
        {
            var dishesByRestaurant = await _restaurantsDbContext.Dishes.FirstOrDefaultAsync(i => i.RestaurantId == id);
            return dishesByRestaurant;
        }
    }
}