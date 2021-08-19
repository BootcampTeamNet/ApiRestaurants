using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Restaurant;
using Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations.Restaurants
{
    public class BranchOfficeService: IBranchOfficeService
    {
        private readonly IUserRestaurantRepository _userRestaurantRepository;
        private readonly IPasswordService _passwordService;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public BranchOfficeService(
            IUserRestaurantRepository userRestaurantRepository,
            IPasswordService passwordService,
            IUserRepository userRepository, 
            IUserService userService,
            IGenericRepository<Restaurant> genericRepository,
            IMapper mapper
            )
        {
            _userRestaurantRepository = userRestaurantRepository;
            _passwordService = passwordService;
            _userRepository = userRepository;
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
                throw new Exception("Los campos no pueden ser nulos");
            }
            
            bool exist = await _userService.ExistsUser(branchOfficeRequestDto.User.Email);
            if (exist)
            {
                throw new Exception($"Ya existe un usuario registrado con el email {branchOfficeRequestDto.User.Email}");
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
