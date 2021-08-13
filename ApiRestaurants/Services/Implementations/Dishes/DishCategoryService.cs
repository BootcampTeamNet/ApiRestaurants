using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Dish;
using Entities;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations.Dishes
{
    public class DishCategoryService : IDishCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;
        
        public DishCategoryService(IGenericRepository<DishCategory> dishCategoryRepository, IMapper mapper)
        {            
            _dishCategoryRepository = dishCategoryRepository;
            _mapper = mapper;
        }

        public async Task<List<DishCategoryRequestDto>> GetAll()
        {
            var dishesCategories = await _dishCategoryRepository.GetAllAsync();
           
            var dishCategoryResponseDto = _mapper.Map<List<DishCategoryRequestDto>>(dishesCategories);
          
            return dishCategoryResponseDto;
        }
    }
}
