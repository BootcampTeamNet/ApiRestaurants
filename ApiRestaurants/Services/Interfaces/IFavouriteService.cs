using DTOs.Favourites;
using DTOs.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFavouriteService
    {
        Task<int> Create(FavouriteRequestDto favouriteRequestDto);
        Task<List<RestaurantMobileResponseDto>> GetFavouriteList(int userId);
    }
}
