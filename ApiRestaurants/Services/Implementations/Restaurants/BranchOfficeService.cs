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
        private readonly IBranchOfficeRepository branchOfficeRepository;
        private readonly IUserService userService;
        private readonly IPasswordService passwordService;
        private readonly IMapper _mapper;

        public BranchOfficeService(
            IBranchOfficeRepository branchOfficeRepository, 
            IUserService userService, 
            IPasswordService passwordService,
            IMapper mapper
            )
        {
            _brandOfficeRepository = branchOfficeRepository;
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
                string.IsNullOrEmpty(branchOfficeRequestDto.MainBranchId)
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

            return await _brandOfficeRepository.Add(UserRestaurant);
        }


    }
}
