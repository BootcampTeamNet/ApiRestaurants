﻿using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IGenericRepository<Restaurant> _genericRepository;
        private readonly IUserService _userService;
        private readonly IUserRestaurantService _userRestaurantService;
        private readonly IMapper _mapper;
        public RestaurantService(IRestaurantRepository restaurantRepository, IGenericRepository<Restaurant> genericRepository, IUserService userService, IUserRestaurantService userRestaurantService,  IMapper mapper)
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
                throw new Exception("Error, todos los campos son requeridos");
            }

            bool exist = await _userService.ExistsUser(restaurantRequestDto.User.Email);
            if (exist)
            {
                throw new Exception($"Ya existe un usuario registrado con el email {restaurantRequestDto.User.Email}");
            }

            return await _userRestaurantService.Add(restaurantRequestDto);
        }


        public async Task<RestaurantResponseDto> GetById(int id)
        {
            var restaurant = await _restaurantRepository.GetById(id);
            if (restaurant == null)
            {
                throw new ArgumentNullException("NotFound");
            }

            var restaurantResponseDto = _mapper.Map<RestaurantResponseDto>(restaurant);
            return restaurantResponseDto;
        }
        public async Task<List<RestaurantResponseDto>> GetAllByCoordinates(double customerLatitude, double customerLongitude)
        {
            //1 milla - 1.609344 km
            //1 grado - 60 min
            //1 milla náutica =  1.1515 millas terrestres
            int distanceKm = 2;
            double gradeToRadian = (Math.PI / 180);
            double radianToGrade = (180 / Math.PI);

            List<Restaurant> lNearRestaurant = new List<Restaurant>();
            var listRestaurant = await _genericRepository.GetAllAsync();

            foreach (var restaurant in listRestaurant)
            {
                var distanceCalculated =
                    (
                        (
                            Math.Acos(
                                Math.Sin(customerLatitude * gradeToRadian) *
                                Math.Sin(Convert.ToDouble(restaurant.LocationLatitude) * gradeToRadian) +
                                Math.Cos(customerLatitude * gradeToRadian) *
                                Math.Cos(Convert.ToDouble(restaurant.LocationLatitude) * gradeToRadian) *
                                Math.Cos((customerLongitude - Convert.ToDouble(restaurant.LocationLongitude)) * gradeToRadian)
                            )
                        ) * radianToGrade
                    ) * 60 * 1.1515 * 1.609344;
                if (distanceCalculated <= distanceKm)
                    lNearRestaurant.Add(restaurant);
            }

            var lrestaurantResponseDto = _mapper.Map<List<RestaurantResponseDto>>(lNearRestaurant);
            return lrestaurantResponseDto;
        }
    }
}
