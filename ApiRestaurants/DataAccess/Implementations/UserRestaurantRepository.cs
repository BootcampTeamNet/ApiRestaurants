using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class UserRestaurantRepository : IUserRestaurantRepository
    {
        private readonly RestaurantsDbContext _context;
        public UserRestaurantRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(UserRestaurant userRestaurant)
        {
            await _context.AddAsync(userRestaurant);
            await _context.SaveChangesAsync();

            return userRestaurant.Restaurant.Id;
        }

        public async Task<UserRestaurant> GetByUserId(int id)
        {
            UserRestaurant response = await (from restaurant in _context.Restaurants
                                             join userRestaurant in _context.UserRestaurants on restaurant.Id equals userRestaurant.RestaurantId
                                             join user in _context.Users on userRestaurant.UserId equals user.Id
                                             where userRestaurant.UserId == id
                                             select new UserRestaurant
                                             {
                                                 UserId = userRestaurant.Id,
                                                 User = user,
                                                 RestaurantId = userRestaurant.RestaurantId,
                                                 Restaurant = restaurant,
                                             }).FirstOrDefaultAsync();
            return response;
        }
    }
}
