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
        
        public async Task<List<Restaurant>> GetFavouriteList(int userId)
        {
            var listFavourite = await (from favourite in _context.Favourites
                                     join restaurant in _context.Restaurants on favourite.RestaurantId equals restaurant.Id
                                     where favourite.UserId == userId
                                     select new Restaurant
                                     {
                                         Id = restaurant.Id,
                                         Name = restaurant.Name,
                                         Address = restaurant.Address,
                                         LocationLatitude = restaurant.LocationLatitude,
                                         LocationLongitude = restaurant.LocationLongitude,
                                         PathImage = restaurant.PathImage,
                                         Email = restaurant.Email,
                                         Phone = restaurant.Phone
                                     }).ToListAsync();
            listFavourite.OrderByDescending(o => o.Name);
            return listFavourite;
        }
    }
}
