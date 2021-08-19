using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System.Threading.Tasks;

namespace Services.Implementations.Restaurants
{
    public class BranchOfficeService: IBranchOfficeService
    {
        private readonly IUserRestaurantRepository _userRestaurantRepository;
        private readonly IPasswordService _passwordService;
        private readonly IUserService _userService;

        public BranchOfficeService(
            IUserRestaurantRepository userRestaurantRepository,
            IPasswordService passwordService,
            IUserService userService
            )
        {
            _userRestaurantRepository = userRestaurantRepository;
            _passwordService = passwordService;
            _userService = userService;
        }

        public async Task<int> Create(BranchOfficeRequestDto branchOfficeRequestDto)
        {
            if (
                string.IsNullOrEmpty(branchOfficeRequestDto.Name) ||
                string.IsNullOrEmpty(branchOfficeRequestDto.Address) ||
                int.Equals(null, branchOfficeRequestDto.MainBranchId)
                )
            {
                throw new EntityBadRequestException("Error, todos los campos son requeridos");
            }
            
            bool exist = await _userService.ExistsUser(branchOfficeRequestDto.User.Email);
            if (exist)
            {
                throw new EntityBadRequestException($"Ya existe un usuario registrado con el email {branchOfficeRequestDto.User.Email}");
            }

            UserRestaurant userRestaurant = new UserRestaurant();
            userRestaurant.Restaurant = new Restaurant
            {
                Name = branchOfficeRequestDto.Name,
                Address = branchOfficeRequestDto.Address,
                LocationLatitude = branchOfficeRequestDto.LocationLatitude.ToString(),
                LocationLongitude = branchOfficeRequestDto.LocationLongitude.ToString(),
                MainBranchId = branchOfficeRequestDto.MainBranchId
            };

            _passwordService.CreatePasswordHash(branchOfficeRequestDto.User.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userRestaurant.User = new User
            {
                FirstName = branchOfficeRequestDto.User.FirstName,
                LastName = branchOfficeRequestDto.User.LastName,
                Email = branchOfficeRequestDto.User.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            return await _userRestaurantRepository.Add(userRestaurant);

        }


    }
}
