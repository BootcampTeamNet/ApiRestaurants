using DTOs.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<int> Create(RegisterRestaurantRequestDto restaurantRequestDto);
        Task<RestaurantResponseDto> GetById(int id);
        Task<List<RestaurantMobileResponseDto>> GetAllByCoordinates(double customerLatitude, double customerLongitude);
    }

}
