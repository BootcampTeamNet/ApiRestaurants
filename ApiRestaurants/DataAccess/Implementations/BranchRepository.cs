using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class BranchRepository: IBranchRepository
    {
        private readonly RestaurantsDbContext _context;
        public BranchRepository(RestaurantsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Restaurant>> GetByRestaurantId(int id)
        {
            return await _context.Restaurants.Where(w => w.MainBranchId == id).ToListAsync();
        }
    }
}
