using AutoMapper;
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
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IGenericRepository<RestaurantCategory> _genericRepository;
        private readonly IMapper _mapper;
        public RestaurantService(IRestaurantRepository restaurantRepository, IUserService userService, IPasswordService passwordService, IGenericRepository<RestaurantCategory> genericRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _userService = userService;
            _passwordService = passwordService;
            _genericRepository = genericRepository;
            _mapper = mapper;

        }
        public async Task<int> Create(RestaurantRequestDto restaurantRequestDto)
        {
            if (string.IsNullOrEmpty(restaurantRequestDto.Name) || string.IsNullOrEmpty(restaurantRequestDto.Address)
                || string.IsNullOrEmpty(restaurantRequestDto.User?.FirstName) || string.IsNullOrEmpty(restaurantRequestDto.User?.LastName)
                || string.IsNullOrEmpty(restaurantRequestDto.User?.Email) || string.IsNullOrEmpty(restaurantRequestDto.User?.Password)) {
                throw new Exception("Los campos no pueden ser nulos");
            }

            bool exist = await _userService.ExistsUser(restaurantRequestDto.User.Email);
            if (exist)
            {
                throw new Exception($"Ya existe un usuario registrado con el email {restaurantRequestDto.User.Email}");
            }

            UserRestaurant userRestaurant = new UserRestaurant();
            userRestaurant.Restaurant = new Restaurant
            {
                Name = restaurantRequestDto.Name,
                Address = restaurantRequestDto.Address,
                LocationLatitude = restaurantRequestDto.LocationLatitude,
                LocationLongitude = restaurantRequestDto.LocationLongitude
            };
            //arma el objeto del usuario para el resturante
            _passwordService.CreatePasswordHash(restaurantRequestDto.User.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userRestaurant.User = new User
            {
                FirstName = restaurantRequestDto.User.FirstName,
                LastName = restaurantRequestDto.User.LastName,
                Email = restaurantRequestDto.User.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            return await _restaurantRepository.Add(userRestaurant);
        }
        public async Task<List<string>> GetList()
        {

            var responseRestaurantCategory = await _genericRepository.GetAllAsync();
            List<string> ListaMachetada = new List<string>();

            foreach ( RestaurantCategory prop in responseRestaurantCategory)
            {
 
                ListaMachetada.Add(prop.Name);
            }
            return ListaMachetada;
        }
    }
}
