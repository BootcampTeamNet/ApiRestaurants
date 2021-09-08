using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Favourites;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using DTOs.Restaurant;
using System.Collections.Generic;
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
        public async Task<List<RestaurantMobileResponseDto>> GetFavouriteList(int userId)
        {
            List<Restaurant> listFavRestaurants = await _favouriteRepository.GetFavouriteList(userId);
            List< RestaurantMobileResponseDto> favResponseDto = _mapper.Map<List<RestaurantMobileResponseDto>>(listFavRestaurants);
            return favResponseDto;
        }

        public async Task<int> DeleteFavouriteList(FavouriteRequestDto favouriteRequestDto)
        {
            int responseDelete = await _favouriteRepository.DeleteFavouriteList(favouriteRequestDto.RestaurantId, favouriteRequestDto.UserId);
            return responseDelete;
        }
        
    }
}
