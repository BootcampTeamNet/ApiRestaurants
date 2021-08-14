using DTOs.Dish;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDishCategoryService
    {
        Task<List<DishCategoryRequestDto>> GetAll();
    }
}
