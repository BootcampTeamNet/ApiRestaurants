using DataAccess.Interfaces;
using DTOs.Restaurant;
using DTOs.Users;
using Entities;
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.Implementations.Restaurants
{
    public class UserRestaurantService : IUserRestaurantService
    {
        private readonly IUserRestaurantRepository _userRestaurantRepository;
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;
        public UserRestaurantService(IUserRestaurantRepository userRestaurantRepository, IUserRepository userRepository, IPasswordService passwordService, IUserService userService)
        {
            _userRestaurantRepository = userRestaurantRepository;
            _passwordService = passwordService;
            _userRepository = userRepository;
        }
        
        public async Task<int> Add(RegisterRestaurantRequestDto restaurantRequestDto)
        {
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

            return await _userRestaurantRepository.Add(userRestaurant);
        }
        public async Task<int> Update(UpdateRestaurantUserRequestDto updateRestaurantUserRequestDto)
        {
            var userRestaurant = await _userRestaurantRepository.GetByUserId(updateRestaurantUserRequestDto.User.Id);
            if(userRestaurant.User.Email != updateRestaurantUserRequestDto.User.Email)
            {
                if (await _userRepository.ExistsUser(updateRestaurantUserRequestDto.User.Email))
                {
                    throw new Exception($"Error, ya existe un usuario con el correo electrónico {updateRestaurantUserRequestDto.User.Email}");
                }
                userRestaurant.User.Email = updateRestaurantUserRequestDto.User.Email;
            }
            if (!string.IsNullOrEmpty(updateRestaurantUserRequestDto.User.Password))
            {
                var verifyPassword = _passwordService.VerifyPasswordHash(updateRestaurantUserRequestDto.User.Password, userRestaurant.User.PasswordHash, userRestaurant.User.PasswordSalt);
                if (!verifyPassword)
                {
                    _passwordService.CreatePasswordHash(updateRestaurantUserRequestDto.User.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    userRestaurant.User.PasswordHash = passwordHash;
                    userRestaurant.User.PasswordSalt = passwordSalt;
                }
            }
            userRestaurant.User.FirstName = updateRestaurantUserRequestDto.User.FirstName;
            userRestaurant.User.LastName = updateRestaurantUserRequestDto.User.LastName;

            userRestaurant.Restaurant.Name = updateRestaurantUserRequestDto.Name;
            userRestaurant.Restaurant.Address = updateRestaurantUserRequestDto.Address;
            userRestaurant.Restaurant.LocationLatitude = updateRestaurantUserRequestDto.LocationLatitude;
            userRestaurant.Restaurant.LocationLongitude = updateRestaurantUserRequestDto.LocationLatitude;
            userRestaurant.Restaurant.ScheduleFrom = updateRestaurantUserRequestDto.ScheduleFrom;
            userRestaurant.Restaurant.ScheduleTo = updateRestaurantUserRequestDto.ScheduleTo;
            userRestaurant.Restaurant.RestaurantCategoryId = updateRestaurantUserRequestDto.RestaurantCategoryId;
            userRestaurant.Restaurant.TimeMaxCancelBooking = updateRestaurantUserRequestDto.TimeMaxCancelBooking;

            return await _userRestaurantRepository.Update(userRestaurant);

        }

    }
}
