using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class FavouriteRepository: IFavouriteRepository
    {
        private readonly RestaurantsDbContext _context;
        public FavouriteRepository(RestaurantsDbContext context)
        {
            _context = context;
        }

        public async Task<Favourite> FindFavorite(Favourite favourite)
        {
            return await _context.Favourites.Where(w => w.UserId == favourite.UserId && w.RestaurantId == favourite.RestaurantId).FirstOrDefaultAsync();
        }
    }
}
