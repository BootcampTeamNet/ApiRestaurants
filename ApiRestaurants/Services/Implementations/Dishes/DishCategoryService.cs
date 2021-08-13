using DataAccess.Interfaces;
using DTOs.Dish;
using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations.Dishes
{
    public class DishCategoryService : IDishCategoryService
    {
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;

        public DishCategoryService(IGenericRepository<DishCategory> dishCategoryRepository)
        {
            _dishCategoryRepository = dishCategoryRepository;
        }

        public async Task<List<DishCategoryRequestDto>> GetAll()
        {
            var dishesCategories = await _dishCategoryRepository.GetAllAsync();
            return (List<DishCategoryRequestDto>)dishesCategories;
        }
    }
}
