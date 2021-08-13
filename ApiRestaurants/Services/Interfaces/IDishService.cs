using DTOs.Dish;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDishService
    {

        Task<List<DishRequestDto>> GetAll();
        Task<int> Create(DishRequestDto dishRequestDto);
        Task<int> Update(DishRequestDto dishRequestDto, int id);
        Task<DishRequestDto> GetById(int id);
    }
}