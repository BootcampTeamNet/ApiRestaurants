using DataAccess.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantsDbContext _context;
        public RestaurantRepository(RestaurantsDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(UserRestaurant userRestaurant)
        {
           await _context.AddAsync(userRestaurant);
           await _context.SaveChangesAsync();
           
           return userRestaurant.Restaurant.Id;
        }
    }
}
