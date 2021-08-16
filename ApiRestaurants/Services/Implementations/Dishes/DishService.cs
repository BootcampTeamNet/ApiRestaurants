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
        private readonly IDishRepository _iDishRepository;
        private readonly IMapper _mapper;
        public DishService(IGenericRepository<Dish> genericRepository, IMapper mapper, IDishRepository iDishRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _iDishRepository = iDishRepository;
        }


        public async Task<bool> ExistsDish(int id)
        {
            return await _iDishRepository.ExistDish(id);
        }
        public Task<List<DishRequestDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Create(DishRequestDto dishRequestDto)
        {
            if (string.IsNullOrEmpty(dishRequestDto.Name))
            {
                throw new Exception("El campo Nombre no puede estar vacío");
            }
            if (string.IsNullOrEmpty(dishRequestDto.Description))
            {
                throw new Exception("El campo Description no puede ser nulo");
            }
            if (dishRequestDto.DishCategoryId <= 0)
            {
                throw new Exception("El Id de la categoria debe ser mayor a cero");
            }
            if (dishRequestDto.RestaurantId <= 0)
            {
                throw new Exception("El Id del restaurante debe ser mayor a cero");
            }

            var data = _mapper.Map<Dish>(dishRequestDto);
            await _genericRepository.Add(data);
            var dishId = data.Id;

            return dishId;
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
                throw new Exception("El Id de la categoría debe ser mayor a cero ");
            }
            if (dishRequestDto.RestaurantId <= 0)
            {
                throw new Exception("El Id del restaurante debe ser mayor a cero");
            }

            var exist = await _iDishRepository.ExistDish(id);

            if (!exist) {
                throw new Exception("El plato con el id "+ id +" no existe");
                throw new ArgumentNullException("NotFound");
            }

            var dish = _mapper.Map<Dish>(dishRequestDto);
            dish.Id = id;

            await _genericRepository.Update(dish);
            return dish.Id;
        }
        public async Task<DishRequestDto> GetById(int id)
        {
            var dish = await _iDishRepository.GetById(id);
            if (dish == null)
            {
                throw new ArgumentNullException("NotFound");
            }

            var dishRequestDto =  _mapper.Map<DishRequestDto>(dish);

            return dishRequestDto;
        }
    }
}