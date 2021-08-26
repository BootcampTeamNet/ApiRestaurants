using DataAccess.Interfaces;
using Entities;
using Services.Interfaces;

namespace Services.Implementations.Favourites
{
    public class FavouriteService: IFavouriteService
    {
        private readonly IGenericRepository<Favourite> _favouriteGenericRepository;
        private readonly IFavouriteRepository _favouriteRepository;
        public FavouriteService(IGenericRepository<Favourite> favouriteGenericRepository,
                                IFavouriteRepository favouriteRepository)
        {
            _favouriteGenericRepository = favouriteGenericRepository;
            _favouriteRepository = favouriteRepository;
        }
    }
}
