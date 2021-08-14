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
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public BranchOfficeService(
            IRestaurantRepository restaurantRepository, 
            IUserService userService, 
            IPasswordService passwordService,
            IMapper mapper
            )
        {
            _restaurantRepository = restaurantRepository;
            _userService = userService;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<int> Create(BranchOfficeRequestDto branchOfficeRequestDto)
        {
            if (
                string.IsNullOrEmpty(branchOfficeRequestDto.Name) ||
                string.IsNullOrEmpty(branchOfficeRequestDto.Address) ||
                string.IsNullOrEmpty(branchOfficeRequestDto.User?.FirstName) ||
                string.IsNullOrEmpty(branchOfficeRequestDto.User?.LastName) ||
                string.IsNullOrEmpty(branchOfficeRequestDto.User?.Email) ||
                string.IsNullOrEmpty(branchOfficeRequestDto.User?.Password) ||
                int.Equals(null, branchOfficeRequestDto.MainBranchId)
                )
            {
                throw new Exception("Los campos no pueden ser nulos");
            }

            bool exist = await _userService.ExistsUser(branchOfficeRequestDto.User.Email);
            if(exist)
            {
                throw new Exception($"Ya existe un usuario registrado con el email {branchOfficeRequestDto.User.Email}");
            }

            UserRestaurant userRestaurant = new UserRestaurant();
            userRestaurant.Restaurant = new Restaurant
            {
                Name = branchOfficeRequestDto.Name,
                Address = branchOfficeRequestDto.Address,
                LocationLatitude = branchOfficeRequestDto.LocationLatitude,
                LocationLongitude = branchOfficeRequestDto.LocationLongitude,
                MainBranchId = branchOfficeRequestDto.MainBranchId
            };

            return await _restaurantRepository.Add(userRestaurant);
        }


    }
}
