using DataAccess.Interfaces;

namespace DataAccess.Implementations
{
    public class FavouriteRepository: IFavouriteRepository
    {
        private readonly RestaurantsDbContext _context;
        public FavouriteRepository(RestaurantsDbContext context)
        {
            _context = context;
        }
    }
}
