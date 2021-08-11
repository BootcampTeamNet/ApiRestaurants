using AutoMapper;
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
        private readonly IMapper _mapper;
        public DishService(IGenericRepository<Dish> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public Task<List<DishRequestDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }
        public Task<DishRequestDto> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Create(DishRequestDto dishRequestDto)
        {
            var data = _mapper.Map<Dish>(dishRequestDto);
            return await _genericRepository.Add(data);
        }


        public async Task<int> Update(DishRequestDto dishRequestDto, int id)
        {
            if (string.IsNullOrEmpty(dishRequestDto.Name)) {
                throw new Exception("El campo Name no puede estar vacío");
            }
            if (string.IsNullOrEmpty(dishRequestDto.Description))
            {
                throw new Exception("El campo Description no puede ser nulo");
            }
            if (dishRequestDto.DishCategoryId<=0)
            {
                throw new Exception("El campo DishCategoryId no puede ser cero");
            }
            if (dishRequestDto.RestaurantId <= 0)
            {
                throw new Exception("El campo RestaurantId no puede ser cero");
            }

            Dish dish = await _genericRepository.GetByIdAsync(id);
            if (dish ==null) {
                throw new Exception("El plato con el id "+ id +" no existe");
            }

            dish.Name = dishRequestDto.Name;
            dish.Description = dishRequestDto.Description;
            dish.Price = dishRequestDto.Price;
            dish.PathImage = dishRequestDto.PathImage;
            dish.CaloriesMinimun = dishRequestDto.CaloriesMinimun;
            dish.CaloriesMaximun = dishRequestDto.CaloriesMaximun;
            dish.Proteins = dishRequestDto.Proteins;
            dish.Fats = dishRequestDto.Fats;
            dish.Sugars = dishRequestDto.Sugars;
            dish.DishCategoryId = dishRequestDto.DishCategoryId;
            dish.RestaurantId = dishRequestDto.RestaurantId;

            return await _genericRepository.Update(dish);
        }

    }
}
