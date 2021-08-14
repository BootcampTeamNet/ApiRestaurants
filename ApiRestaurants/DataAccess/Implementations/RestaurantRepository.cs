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
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantsDbContext _context;

        public RestaurantRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<Restaurant> GetById(int id) 
        {
            Restaurant rest = new Restaurant();
            rest = await _context.Restaurants.FirstOrDefaultAsync(
                i => i.Id == id);
            return rest;
        }
    }
}
