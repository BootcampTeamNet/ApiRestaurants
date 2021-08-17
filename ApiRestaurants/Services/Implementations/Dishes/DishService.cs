using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Dish;
using Entities;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations.Dishes
{
    public class DishService : IDishService
    {
        private readonly IGenericRepository<Dish> _genericRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IFileService _fileService;
        private readonly IStringProcess _stringProcess;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IDishRepository _iDishRepository;
        public DishService(IGenericRepository<Dish> genericRepository, IRestaurantRepository restaurantRepository,
            IFileService fileService, IStringProcess stringProcess, IMapper mapper, IConfiguration configuration, IDishRepository dishRepository)
        {
            _genericRepository = genericRepository;
            _restaurantRepository = restaurantRepository;
            _fileService = fileService;
            _stringProcess = stringProcess;
            _mapper = mapper;
            _configuration = configuration;
            _iDishRepository = dishRepository;
        }

        public Task<List<DishRequestDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Create(DishRequestDto dishRequestDto)
        {
            Validation(dishRequestDto);
            var data = _mapper.Map<Dish>(dishRequestDto);
            //guardar imagen
            if (dishRequestDto.Image != null)
            {
                Restaurant restaurant = await _restaurantRepository.GetById(dishRequestDto.RestaurantId);
                string filePath = restaurant.Id + _stringProcess.removeSpecialCharacter(restaurant.Name);
                string imageFullPath = $"{_configuration.GetSection("FileServer:path").Value}{filePath}\\{dishRequestDto.Image.FileName}";
                await _fileService.SaveFile(dishRequestDto.Image, filePath);
                data.PathImage = imageFullPath;
            }

            await _genericRepository.Add(data);

            return data.Id;
        }

        public async Task<int> Update(int id, DishRequestDto dishRequestDto)
        {
            Validation(dishRequestDto);

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
            //guardar imagen
            if (dishRequestDto.Image != null)
            {
                Restaurant restaurant = await _restaurantRepository.GetById(dish.RestaurantId);
                string filePath = restaurant.Id + _stringProcess.removeSpecialCharacter(restaurant.Name);
                string imageFullPath = $"{_configuration.GetSection("FileServer:path").Value}{filePath}\\{dishRequestDto.Image.FileName}";
                await _fileService.SaveFile(dishRequestDto.Image, filePath);
                dish.PathImage = imageFullPath;
            }
            else {
                dish.PathImage = null;
            }

            dish.Name = dishRequestDto.Name;
            dish.Description = dishRequestDto.Description;
            dish.Price = dishRequestDto.Price;
            dish.CaloriesMinimun = dishRequestDto.CaloriesMinimun??0;
            dish.CaloriesMaximun = dishRequestDto.CaloriesMaximun??0;
            dish.Proteins = dishRequestDto.Proteins??0;
            dish.Fats = dishRequestDto.Fats??0;
            dish.Sugars = dishRequestDto.Sugars??0;
            dish.DishCategoryId = dishRequestDto.DishCategoryId;
            
            await _genericRepository.Update(dish);

            return dish.Id;
        }
        public async Task<int> Status(int id, int restaurantId)
        {
            Dish dish = await _genericRepository.GetByIdAsync(id);
            if (dish == null)
            {
                throw new Exception($"El plato con el id {id} no existe");
            }
            if (restaurantId != dish.RestaurantId)
            {
                throw new Exception($"Error, no puede modificar platos de la sucursal principal");
            }

            if (dish.IsActive)
            {
                dish.IsActive = false;
            }
            else
            {
                dish.IsActive = true;
            }
            await _genericRepository.Update(dish);
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
        public async Task<DishResponseDto> GetById(int id)
        {
            var dish = await _genericRepository.GetByIdAsync(id);
            if (dish == null)
            {
                throw new ArgumentNullException("NotFound");
            }

            var dishRequestDto =  _mapper.Map<DishResponseDto>(dish);
            return dishRequestDto;
        }
        public async Task<List<DishesByRestaurantResponseDto>> GetListByIdRestaurant(int id)
        {
            var responseListByIdRestaurant = await _iDishRepository.GetListByIdRestaurant(id);
            if (responseListByIdRestaurant == null) {
                throw new ArgumentNullException();
            }
            var response = _mapper.Map<List<DishesByRestaurantResponseDto>>(responseListByIdRestaurant);
            return response;
        }
    }
}
