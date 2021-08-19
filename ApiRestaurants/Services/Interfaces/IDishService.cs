using DTOs.Dish;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDishService
    {
        Task<int> Create(DishRequestDto dishRequestDto);
        Task<int> Update(int id, DishRequestDto dishRequestDto);
        Task<int> Status(int id, int restaurantId);
        Task<List<DishResponseDto>> GetActiveDishList(int id);
        Task<DishResponseDto> GetById(int id);
        Task<List<DishesByRestaurantResponseDto>> GetAllByRestaurantId(int id);
    }
}