using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Favourites;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
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
            _mapper = mapper;
        }

        public async Task<int> Create(FavouriteRequestDto favouriteRequestDto)
        {
            Favourite favouriteNew = _mapper.Map<Favourite>(favouriteRequestDto);
            Favourite favourite = await _favouriteRepository.FindFavorite(favouriteNew);
            if (favourite != null) 
            {
                throw new EntityBadRequestException("El restaurante ya esta entre sus favoritos");
            }
            
            await _favouriteGenericRepository.Add(favouriteNew);
            
            return favouriteNew.Id;
        }
    }
}
