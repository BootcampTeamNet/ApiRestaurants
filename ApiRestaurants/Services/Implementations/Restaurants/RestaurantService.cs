using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IGenericRepository<Restaurant> _genericRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserService _userService;
        private readonly IUserRestaurantService _userRestaurantService;
        private readonly IStringProcess _stringProcess;
        private readonly IToStockAFile _toStockAFile;
        private readonly IMapper _mapper;
        public RestaurantService(
            IGenericRepository<Restaurant> genericRepository
            , IRestaurantRepository restaurantRepository
            , IUserService userService
            , IUserRestaurantService userRestaurantService
            , IStringProcess stringProcess
            , IToStockAFile toStockAFile
            , IMapper mapper)
        {
            _genericRepository = genericRepository;
            _restaurantRepository = restaurantRepository;
            _userService = userService;
            _userRestaurantService = userRestaurantService;
            _stringProcess = stringProcess;
            _toStockAFile = toStockAFile;
            _mapper = mapper;

        }
        public async Task<int> Create(RegisterRestaurantRequestDto restaurantRequestDto)
        {
            if (string.IsNullOrEmpty(restaurantRequestDto.Name) || string.IsNullOrEmpty(restaurantRequestDto.Address)
                || string.IsNullOrEmpty(restaurantRequestDto.User?.FirstName) || string.IsNullOrEmpty(restaurantRequestDto.User?.LastName)
                || string.IsNullOrEmpty(restaurantRequestDto.User?.Email) || string.IsNullOrEmpty(restaurantRequestDto.User?.Password)) {
                throw new EntityBadRequestException("Error, todos los campos son requeridos");
            }

            bool exist = await _userService.ExistsUser(restaurantRequestDto.User.Email);
            if (exist)
            {
                throw new EntityBadRequestException($"Ya existe un usuario registrado con el email {restaurantRequestDto.User.Email}");
            }

            int restaurantId = await _userRestaurantService.Add(restaurantRequestDto);

            if (restaurantRequestDto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    Restaurant restaurant = await _genericRepository.GetByIdAsync(restaurantId);
                    string filePath = _stringProcess.removeSpecialCharacter(restaurant.Name) + restaurant.Id;
                    string container = filePath.ToLower();
                    await restaurantRequestDto.Image.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extention = Path.GetExtension(restaurantRequestDto.Image.FileName);
                    restaurant.PathImage = await _toStockAFile.SaveFile(
                        content,
                        extention,
                        container,
                        restaurantRequestDto.Image.ContentType
                        );
                    await _genericRepository.Update(restaurant);
                }
            }

            return restaurantId;
        }

        public async Task<RestaurantResponseDto> GetById(int id)
        {
            var restaurant = await _genericRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                throw new EntityNotFoundException($"No existe el restaurante de id { id }");
            }
            UserRestaurant userRestaurant = await _userRestaurantService.GetByRestaurantId(id);
            RestaurantResponseDto response = _mapper.Map<RestaurantResponseDto>(userRestaurant.Restaurant);
            response.User = _mapper.Map<UserResponseDto>(userRestaurant.User);
            return response;
        }
        public async Task<List<RestaurantMobileResponseDto>> GetAllByCoordinates(double customerLatitude, double customerLongitude)
        {
            List<Restaurant> closestRestaurant = await _restaurantRepository.RestaurantsByCoordinates(customerLatitude, customerLongitude);
            List<RestaurantMobileResponseDto> lrestaurantResponseDto = _mapper.Map<List<RestaurantMobileResponseDto>>(closestRestaurant);
            return lrestaurantResponseDto;
        }
        public async Task<List<RestaurantMobileResponseDto>> GetAllByKeyWord(FilterRestaurantRequestDto filterRequestDto)
        {
            if (string.IsNullOrEmpty(filterRequestDto.KeyWord) || filterRequestDto.KeyWord.Length<3) {
                throw new EntityBadRequestException("Debe ingresar al menos 3 caracteres para la búsqueda");
            }

            List<Restaurant> closestRestaurant = await _restaurantRepository.RestaurantsByKeyWord(filterRequestDto.CustomerLatitude, filterRequestDto.CustomerLongitude,filterRequestDto.KeyWord);
            List<RestaurantMobileResponseDto> lrestaurantResponseDto = _mapper.Map<List<RestaurantMobileResponseDto>>(closestRestaurant);
            return lrestaurantResponseDto;
        }

        public async Task<List<RestaurantMobileResponseDto>> GetByDishesFilter(FilterByDishesRequestDto filterRequestDto) {
            List<Restaurant> closestRestaurants = await _restaurantRepository.RestauranstByDishCategory(
                filterRequestDto.CustomerLatitude,
                filterRequestDto.CustomerLongitude,
                filterRequestDto.DishCategoriesIdList,
                filterRequestDto.WithLocation);
            List<RestaurantMobileResponseDto> response = _mapper.Map<List<RestaurantMobileResponseDto>>(closestRestaurants);
            return response;
        }
    }
}
