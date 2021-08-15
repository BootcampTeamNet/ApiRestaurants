using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Dish;
using Entities;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Implementations.Dishes
{
    public class DishService : IDishService
    {
        private readonly IGenericRepository<Dish> _genericRepository;
        private readonly IDishRepository _iDishRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IFileService _fileService;
        private readonly IStringProcess _stringProcess;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public DishService(IGenericRepository<Dish> genericRepository, IMapper mapper,
            IRestaurantRepository restaurantRepository, IDishRepository iDishRepository, IFileService fileService, 
            IStringProcess stringProcess, IConfiguration configuration)
        {
            _genericRepository = genericRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _iDishRepository = iDishRepository;
            _fileService = fileService;
            _stringProcess = stringProcess;
            _configuration = configuration;
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
            Validation(dishRequestDto);

            var data = _mapper.Map<Dish>(dishRequestDto);
            await _genericRepository.Add(data);
            var dishId = data.Id;

            return dishId;
        }

        public async Task<int> Update(int id, UpdateDishRequestDto dishRequestDto)
        {
            //Validation(dishRequestDto);

            Dish dish = await _genericRepository.GetByIdAsync(id);
            if (dish == null)
            {
                throw new Exception($"El plato con el id {id} no existe");
            }
            //una sucursal solo puede modificar sus propios platos, no platos de la sucursal principal
            if (dishRequestDto.RestaurantId != dish.RestaurantId) {
                throw new Exception($"Error, no puede modificar platos de la sucursal principal");
            }
            if (!string.IsNullOrEmpty(dish.PathImage))
            {
                _fileService.DeleteFile(dish.PathImage);
            }

            var dishUpate = _mapper.Map<Dish>(dishRequestDto);
            dishUpate.Id = id;

            if (dishRequestDto.Image != null)
            {
                Restaurant restaurant = await _restaurantRepository.GetById(dish.RestaurantId);
                string filePath = restaurant.Id + _stringProcess.removeSpecialCharacter(restaurant.Name);
                string imageFullPath = $"{_configuration.GetSection("FileServer:path").Value}{filePath}\\{dishRequestDto.Image.FileName}";
                await _fileService.SaveFile(dishRequestDto.Image, filePath);
                dishUpate.PathImage = imageFullPath;
            }
            else {
                dishUpate.PathImage = "";
            }

            await _genericRepository.Update(dishUpate);
            return dish.Id;
        }

        private static void Validation(DishRequestDto dishRequestDto)
        {
            if (string.IsNullOrEmpty(dishRequestDto.Name))
            {
                throw new Exception("El campo Name no puede estar vacío");
            }
            if (string.IsNullOrEmpty(dishRequestDto.Description))
            {
                throw new Exception("El campo Description no puede ser nulo");
            }
            if (dishRequestDto.DishCategoryId <= 0)
            {
                throw new Exception("El Id de la categoría debe ser mayor a cero ");
            }
            if (dishRequestDto.RestaurantId <= 0)
            {
                throw new Exception("El Id del restaurante debe ser mayor a cero");
            }
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
