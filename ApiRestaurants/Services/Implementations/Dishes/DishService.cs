using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Dish;
using Entities;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Services.Implementations.Dishes
{
    public class DishService : IDishService
    {
        private readonly IGenericRepository<Dish> _genericRepository;
        private readonly IGenericRepository<Restaurant> _restaurantRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;
        private readonly IFileService _fileService;
        private readonly IStringProcess _stringProcess;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IToStockAFile _toStockAFile;
        //private readonly string _container = "dishes";
        public DishService(IGenericRepository<Dish> genericRepository, IGenericRepository<Restaurant> restaurantRepository,
             IDishRepository dishRepository, IGenericRepository<DishCategory> dishCategoryRepository,
            IFileService fileService, IStringProcess stringProcess, IMapper mapper, IConfiguration configuration,
            IToStockAFile toStockAFile
            )
        {
            _genericRepository = genericRepository;
            _restaurantRepository = restaurantRepository;
            _dishRepository = dishRepository;
            _dishCategoryRepository = dishCategoryRepository;
            _fileService = fileService;
            _stringProcess = stringProcess;
            _mapper = mapper;
            _configuration = configuration;
            _toStockAFile = toStockAFile;
            
        }

        public async Task<int> Create(DishRequestDto dishRequestDto)
        {
            await Validation(dishRequestDto);
            var data = _mapper.Map<Dish>(dishRequestDto);
            //guardar imagen
            if (dishRequestDto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    string container = "dish";
                    await dishRequestDto.Image.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extention = Path.GetExtension(dishRequestDto.Image.FileName);
                    data.PathImage = await _toStockAFile.SaveFile(
                        content,
                        extention,
                        container,
                        dishRequestDto.Image.ContentType
                        );
                }
                /*
                Restaurant restaurant = await _restaurantRepository.GetByIdAsync(dishRequestDto.RestaurantId);
                string filePath = restaurant.Id + _stringProcess.removeSpecialCharacter(restaurant.Name);
                string imageFullPath = $"{_configuration.GetSection("FileServer:path").Value}{filePath}\\{dishRequestDto.Image.FileName}";
                await _fileService.SaveFile(dishRequestDto.Image, filePath);
                data.PathImage = imageFullPath;
                */
            }

            await _genericRepository.Add(data);

            return data.Id;
        }

        public async Task<int> Update(int id, DishRequestDto dishRequestDto)
        {
            await Validation(dishRequestDto);

            Dish dish = await _genericRepository.GetByIdAsync(id);
            if (dish == null)
            {
                throw new EntityNotFoundException($"El plato con el id {id} no existe");
            }
            //una sucursal solo puede modificar sus propios platos, no platos de la sucursal principal
            if (dishRequestDto.RestaurantId != dish.RestaurantId) {
                throw new InaccessibleResourceException($"Error, no puede modificar platos de la sucursal principal");
            }
            if (!string.IsNullOrEmpty(dish.PathImage))
            {
                _fileService.DeleteFile(dish.PathImage);
            }
            //guardar imagen
            if (dishRequestDto.Image != null)
            {
                Restaurant restaurant = await _restaurantRepository.GetByIdAsync(dish.RestaurantId);
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
                throw new EntityNotFoundException($"El plato con el id {id} no existe");
            }
            if (restaurantId != dish.RestaurantId)
            {
                throw new InaccessibleResourceException($"Error, no puede modificar platos de la sucursal principal");
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

        public async Task<List<DishByCategoryResponseDto>> GetActiveDishList(int restaurantId)
        {
            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
            {
                throw new EntityNotFoundException($"Error, no se encuentra el restaurante con {restaurantId}");
            }
            List<Dish> dishes = await _dishRepository.GetActiveDishList(restaurantId);
            var dishesGroupByCategory = dishes.GroupBy(g => new { g.DishCategory.Id, g.DishCategory.Name })
                                        .Select(s => new DishByCategoryResponseDto{ 
                                            Id= s.Key.Id,
                                            Name = s.Key.Name,
                                            Dishes = _mapper.Map <List<DishDto>>(s.ToList())
                                        })     
                                        .ToList();
            //var response = _mapper.Map<List<DishResponseDto>>(dishes);
            return dishesGroupByCategory;
        }

        private async Task Validation(DishRequestDto dishRequestDto)
        {
            if (string.IsNullOrEmpty(dishRequestDto.Name))
            {
                throw new EntityBadRequestException("El campo nombre del plato no puede estar vacío");
            }
            if (string.IsNullOrEmpty(dishRequestDto.Description))
            {
                throw new EntityBadRequestException("Debe ingresar una descripción del plato");
            }
            bool existCategory = await _dishCategoryRepository.Exist(dishRequestDto.DishCategoryId);
            if (!existCategory)
            {
                throw new EntityNotFoundException("Seleccione un Id categoría restaurante que exista");
            }
            bool existRestaurant = await _restaurantRepository.Exist(dishRequestDto.RestaurantId);
            if (!existRestaurant)
            {
                throw new EntityNotFoundException("Seleccione un Id de restaurante que exista");
            }
        }
        public async Task<DishResponseDto> GetById(int id)
        {
            var dish = await _genericRepository.GetByIdAsync(id);
            if (dish == null)
            {
                throw new EntityNotFoundException($"El plato con el id {id} no existe");
            }

            var dishRequestDto =  _mapper.Map<DishResponseDto>(dish);
            return dishRequestDto;
        }
        public async Task<List<DishesByRestaurantResponseDto>> GetAllByRestaurantId(int id)
        {
            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null) {
                throw new EntityNotFoundException($"No existe el restaurante de id {id}");
            }
            var responseListByIdRestaurant = await _dishRepository.GetListByIdRestaurant(id);
            var response = _mapper.Map<List<DishesByRestaurantResponseDto>>(responseListByIdRestaurant);
            return response;
        }
    }
}