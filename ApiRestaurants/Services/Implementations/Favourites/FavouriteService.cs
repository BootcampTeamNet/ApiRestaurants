using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Favourites;
using Entities;
using Services.Interfaces;
using System.Threading.Tasks;

namespace Services.Implementations.Favourites
{
    public class FavouriteService: IFavouriteService
    {
        private readonly IGenericRepository<Favourite> _favouriteGenericRepository;
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly IMapper _mapper;
        public FavouriteService(IGenericRepository<Favourite> favouriteGenericRepository,
                                IFavouriteRepository favouriteRepository,
                                IMapper mapper)
        {
            _favouriteGenericRepository = favouriteGenericRepository;
            _favouriteRepository = favouriteRepository;
        }

        public async Task<int> Create(FavouriteRequestDto favouriteRequestDto)
        {
            Favourite favourite = new Favourite
            {
                UserId = favouriteRequestDto.UserId,
                RestaurantId = favouriteRequestDto.RestaurantId
            };

            return await _favouriteGenericRepository.Add(favourite);
        }
    }
}
