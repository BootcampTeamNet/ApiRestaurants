using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using System.Threading.Tasks;

namespace Services.Implementations.Restaurants
{
    public class UserRestaurantService : IUserRestaurantService
    {
        private readonly IUserRestaurantRepository _userRestaurantRepository;
        private readonly IPasswordService _passwordService;
        public UserRestaurantService(IUserRestaurantRepository userRestaurantRepository, IPasswordService passwordService)
        {
            _userRestaurantRepository = userRestaurantRepository;
            _passwordService = passwordService;
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
    }
}
