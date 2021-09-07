using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IGenericRepository<Restaurant> _genericRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserService _userService;
        private readonly IUserRestaurantService _userRestaurantService;
        private readonly IMapper _mapper;
        public RestaurantService(
            IGenericRepository<Restaurant> genericRepository
            , IRestaurantRepository restaurantRepository
            , IUserService userService
            , IUserRestaurantService userRestaurantService
            , IMapper mapper)
        {
            _genericRepository = genericRepository;
            _restaurantRepository = restaurantRepository;
            _userService = userService;
            _userRestaurantService = userRestaurantService;
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

            return await _userRestaurantService.Add(restaurantRequestDto);
        }

        public async Task<RestaurantResponseDto> GetById(int id)
        {
            var restaurant = await _genericRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                throw new EntityNotFoundException($"No existe el restaurante de id { id }");
            }

            var restaurantResponseDto = _mapper.Map<RestaurantResponseDto>(restaurant);
            return restaurantResponseDto;
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
