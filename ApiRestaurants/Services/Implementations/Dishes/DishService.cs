using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Dish;
using Entities;
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
        private readonly IStringProcess _stringProcess;
        private readonly IMapper _mapper;
        private readonly IToStockAFile _toStockAFile;

        public DishService(IGenericRepository<Dish> genericRepository, IGenericRepository<Restaurant> restaurantRepository,
             IDishRepository dishRepository, IGenericRepository<DishCategory> dishCategoryRepository,
             IStringProcess stringProcess, IMapper mapper,
             IToStockAFile toStockAFile
            )
        {
            _genericRepository = genericRepository;
            _restaurantRepository = restaurantRepository;
            _dishRepository = dishRepository;
            _dishCategoryRepository = dishCategoryRepository;
            _stringProcess = stringProcess;
            _mapper = mapper;
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
                    Restaurant restaurant = await _restaurantRepository.GetByIdAsync(dishRequestDto.RestaurantId);
                    string filePath = _stringProcess.removeSpecialCharacter(restaurant.Name) + restaurant.Id;
                    string container = filePath.ToLower();
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
            }

            await _genericRepository.Add(data);

            return data.Id;
        }

        public async Task<int> Update(int id, DishRequestDto dishRequestDto)
        {
            await Validation(dishRequestDto);
            
            Dish dish = await _genericRepository.GetByIdAsync(id);
            string route = dish.PathImage;
            if (dish == null)
            {
                throw new EntityNotFoundException($"El plato con el id {id} no existe");
            }
            //una sucursal solo puede modificar sus propios platos, no platos de la sucursal principal
            if (dishRequestDto.RestaurantId != dish.RestaurantId) {
                throw new InaccessibleResourceException($"Error, no puede modificar platos de la sucursal principal");
            }

            //guardar imagen
            if (dishRequestDto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {                    
                    Restaurant restaurant = await _restaurantRepository.GetByIdAsync(dishRequestDto.RestaurantId);
                    string filePath = _stringProcess.removeSpecialCharacter(restaurant.Name) + restaurant.Id;
                    string container = filePath.ToLower();

                    await dishRequestDto.Image.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extention = Path.GetExtension(dishRequestDto.Image.FileName);
                    dish.PathImage = await _toStockAFile.SaveFile(
                        content,
                        extention,
                        container,
                        dishRequestDto.Image.ContentType
                        );
                }
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

            //en las sucursales, se listan los platos del restaurante principal + sus propios platos
            if (restaurant.MainBranchId != null)
            {
                List<Dish> listDishesbyMainRestaurant = await _dishRepository.GetActiveDishList((int)restaurant.MainBranchId);
                dishes.AddRange(listDishesbyMainRestaurant);
            }

            var dishesGroupByCategory = dishes.GroupBy(g => new { g.DishCategory.Id, g.DishCategory.Name })
                                        .Select(s => new DishByCategoryResponseDto{ 
                                            Id= s.Key.Id,
                                            Name = s.Key.Name,
                                            Dishes = _mapper.Map <List<DishDto>>(s.ToList())
                                        })     
                                        .ToList();
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
            List<Dish> listDishes = await _dishRepository.GetListByIdRestaurant(id);

            //en las sucursales, se listan los platos del restaurante principal + sus propios platos
            if (restaurant.MainBranchId != null) {
                List<Dish> listDishesbyMainRestaurant = await _dishRepository.GetListByIdRestaurant((int)restaurant.MainBranchId);

                listDishes.AddRange(listDishesbyMainRestaurant);
            }
            var response = _mapper.Map<List<DishesByRestaurantResponseDto>>(listDishes);
            return response;
        }
    }
}