using DTOs.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<int> Create(RestaurantRequestDto restaurantRequestDto);
        Task<RestaurantResponseDto> GetById(int id);
        Task<List<RestaurantCategoryRequestDto>> GetList();
    }

}
