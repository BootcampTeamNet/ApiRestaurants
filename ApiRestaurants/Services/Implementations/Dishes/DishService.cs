using DataAccess.Interfaces;
using DTOs.Dish;
using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations.Dishes
{
    public class DishService : IDishService
    {
        private readonly IGenericRepository<Dish> _genericRepository;
        public DishService(IGenericRepository<Dish> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public Task<List<DishRequestDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }
        public Task<DishRequestDto> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Create(DishRequestDto dishRequestDto)
        {
            throw new System.NotImplementedException();
        }


        public async Task<int> Update(DishRequestDto dishRequestDto, int id)
        {
            Dish dish = await _genericRepository.GetByIdAsync(id);
            if (dish ==null) {
                throw new Exception("El plato con el id "+ id +" no existe");
            }

            Dish dishUpdate = new Dish
            {
                Id = id,
                Name = dishRequestDto.Name,
                Description = dishRequestDto.Description,
                Price = dishRequestDto.Price,
                PathImage=dishRequestDto.PathImage,
                CaloriesMinimun = dishRequestDto.CaloriesMinimun,
                CaloriesMaximun = dishRequestDto.CaloriesMaximun,
                Proteins = dishRequestDto.Proteins,
                Fats = dishRequestDto.Fats,
                Sugars = dishRequestDto.Sugars,
                DishCategoryId = dishRequestDto.DishCategoryId,
                RestaurantId = dishRequestDto.RestaurantId
            };

            return await _genericRepository.Update(dishUpdate);
        }

    }
}
