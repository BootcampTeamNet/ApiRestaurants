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
    }
}