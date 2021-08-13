using DTOs.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    public interface IRestaurantCategoryService
    {
        Task<List<RestaurantCategoryRequestDto>> GetList();
    }
}
