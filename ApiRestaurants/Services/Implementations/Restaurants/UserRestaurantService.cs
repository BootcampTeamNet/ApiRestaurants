using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System.IO;
using System.Threading.Tasks;

namespace Services.Implementations.Restaurants
{
    public class UserRestaurantService : IUserRestaurantService
    {
        private readonly IUserRestaurantRepository _userRestaurantRepository;
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;
        private readonly IToStockAFile _toStockAFile;
        private readonly IStringProcess _stringProcess;
        public UserRestaurantService(IUserRestaurantRepository userRestaurantRepository, 
            IUserRepository userRepository, 
            IPasswordService passwordService,
            IToStockAFile toStockAFile,
            IStringProcess stringProcess)
        {
            _userRestaurantRepository = userRestaurantRepository;
            _passwordService = passwordService;
            _userRepository = userRepository;
            _toStockAFile = toStockAFile;
            _stringProcess = stringProcess;;
    }
        
        public async Task<int> Add(RegisterRestaurantRequestDto restaurantRequestDto)
        {
            UserRestaurant userRestaurant = new UserRestaurant();
            userRestaurant.Restaurant = new Restaurant
            {
                Name = restaurantRequestDto.Name,
                Address = restaurantRequestDto.Address,
                LocationLatitude = restaurantRequestDto.LocationLatitude.ToString(),
                LocationLongitude = restaurantRequestDto.LocationLongitude.ToString()
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
                    throw new EntityBadRequestException($"Error, ya existe un usuario con el correo electrónico {updateRestaurantUserRequestDto.User.Email}");
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

            //guardar imagen
            if (updateRestaurantUserRequestDto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    
                    string filePath = _stringProcess.removeSpecialCharacter(userRestaurant.Restaurant.Name) + userRestaurant.Restaurant.Id;
                    string container = filePath.ToLower();
                    string route = userRestaurant.Restaurant.PathImage;
                    await updateRestaurantUserRequestDto.Image.CopyToAsync(memoryStream);

                    var content = memoryStream.ToArray();
                    var extention = Path.GetExtension(updateRestaurantUserRequestDto.Image.FileName);
                    userRestaurant.Restaurant.PathImage = await _toStockAFile.EditFile(
                        content,
                        extention,
                        container,
                        route,
                        updateRestaurantUserRequestDto.Image.ContentType
                        );
                }
            }
            
            userRestaurant.User.FirstName = updateRestaurantUserRequestDto.User.FirstName;
            userRestaurant.User.LastName = updateRestaurantUserRequestDto.User.LastName;

            userRestaurant.Restaurant.Name = updateRestaurantUserRequestDto.Name;
            userRestaurant.Restaurant.Address = updateRestaurantUserRequestDto.Address;
            userRestaurant.Restaurant.LocationLatitude = updateRestaurantUserRequestDto.LocationLatitude;
            userRestaurant.Restaurant.LocationLongitude = updateRestaurantUserRequestDto.LocationLongitude;
            userRestaurant.Restaurant.ScheduleFrom = updateRestaurantUserRequestDto.ScheduleFrom;
            userRestaurant.Restaurant.ScheduleTo = updateRestaurantUserRequestDto.ScheduleTo;
            userRestaurant.Restaurant.RestaurantCategoryId = updateRestaurantUserRequestDto.RestaurantCategoryId;
            userRestaurant.Restaurant.TimeMaxCancelBooking = updateRestaurantUserRequestDto.TimeMaxCancelBooking;

            return await _userRestaurantRepository.Update(userRestaurant);

        }

        public async Task<UserRestaurant> GetByRestaurantId(int id) {

            return await _userRestaurantRepository.GetByRestaurantId(id);
        }
    }
}
