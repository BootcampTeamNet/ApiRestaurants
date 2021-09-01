using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations.Restaurants
{
    public class BranchOfficeService: IBranchOfficeService
    {
        private readonly IUserRestaurantRepository _userRestaurantRepository;
        private readonly IGenericRepository<Restaurant> _restaurantGenericRepository;
        private readonly IPasswordService _passwordService;
        private readonly IUserService _userService;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public BranchOfficeService(
            IUserRestaurantRepository userRestaurantRepository,
            IGenericRepository<Restaurant> restaurantGenericRepository,
            IPasswordService passwordService,
            IUserService userService,
            IBranchRepository branchRepository,
            IMapper mapper
            )
        {
            _userRestaurantRepository = userRestaurantRepository;
            _restaurantGenericRepository = restaurantGenericRepository;
            _passwordService = passwordService;
            _userService = userService;
            _branchRepository = branchRepository;
            _mapper = mapper;
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

            Restaurant restaurant  = await _restaurantGenericRepository.GetByIdAsync(branchOfficeRequestDto.MainBranchId);
            if (restaurant==null)
            {
                throw new EntityNotFoundException($"No existe un restaurante con el id {branchOfficeRequestDto.MainBranchId}");
            }

            UserRestaurant userRestaurant = new UserRestaurant();
            userRestaurant.Restaurant = new Restaurant
            {
                Name = restaurant.Name + " - " + branchOfficeRequestDto.Name,
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

        public async Task<List<RestaurantResponseDto>> GetByRestaurantId(int id) 
        {
            List<Restaurant> restaurants = await _branchRepository.GetByRestaurantId(id);
            List<RestaurantResponseDto> response = _mapper.Map<List<RestaurantResponseDto>>(restaurants);
            return response;
        }

    }
}
