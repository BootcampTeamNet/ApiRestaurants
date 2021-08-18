using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IGenericRepository<Restaurant> _genericRepository;
        private readonly IUserService _userService;
        private readonly IUserRestaurantService _userRestaurantService;
        private readonly IMapper _mapper;
        public RestaurantService(IGenericRepository<Restaurant> genericRepository, IUserService userService, IUserRestaurantService userRestaurantService,  IMapper mapper)
        {
            _genericRepository = genericRepository;
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
            //1 milla - 1.609344 km
            //1 grado - 60 min
            //1 milla náutica =  1.1515 millas terrestres
            int distanceKm = 5;
            double gradeToRadian = (Math.PI / 180);
            double radianToGrade = (180 / Math.PI);

            List<Restaurant> lNearRestaurant = new List<Restaurant>();
            var listRestaurant = await _genericRepository.GetAllAsync();

            foreach (var restaurant in listRestaurant)
            {
                var result = Math.Sin(customerLatitude * gradeToRadian) *
                                Math.Sin(Convert.ToDouble(restaurant.LocationLatitude) * gradeToRadian) +
                                Math.Cos(customerLatitude * gradeToRadian) *
                                Math.Cos(Convert.ToDouble(restaurant.LocationLatitude) * gradeToRadian) *
                                Math.Cos((customerLongitude - Convert.ToDouble(restaurant.LocationLongitude)) * gradeToRadian);
                // range cos [-1,1]
                if (result > 1)
                    result = 1;
                if (result <-1)
                    result = -1;

                var distanceCalculated = (Math.Acos(result) * radianToGrade) * 60 * 1.1515 * 1.609344;
                if (distanceCalculated <= distanceKm)
                    lNearRestaurant.Add(restaurant);
            }

            var lrestaurantResponseDto = _mapper.Map<List<RestaurantMobileResponseDto>>(lNearRestaurant);
            return lrestaurantResponseDto;
        }
    }
}
