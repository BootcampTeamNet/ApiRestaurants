using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<UserRestaurant> GetByUserId(int id)
        {
            UserRestaurant response = await (from userRestaurant in _context.UserRestaurants
                                             join restaurant in _context.Restaurants on userRestaurant.Id equals restaurant.Id
                                             where userRestaurant.UserId == id
                                             select new UserRestaurant
                                             {
                                                 UserId = userRestaurant.Id,
                                                 RestaurantId = userRestaurant.RestaurantId,
                                                 Restaurant = restaurant,
                                             }).FirstOrDefaultAsync();
            return response;
        }
    }
}
