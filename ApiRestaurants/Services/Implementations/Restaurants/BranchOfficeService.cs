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
        //private readonly IRestaurantRepository _restaurantRepository;
        //private readonly IUserService _userService;
        //private readonly IPasswordService _passwordService;
        private readonly IGenericRepository<Restaurant> _genericRepository;
        private readonly IMapper _mapper;

        public BranchOfficeService(
            //IRestaurantRepository restaurantRepository,
            //IUserService userService,
            //IPasswordService passwordService,
            IGenericRepository<Restaurant> genericRepository,
            IMapper mapper
            )
        {
            //_restaurantRepository = restaurantRepository;
            //_userService = userService;
            //_passwordService = passwordService;
            _genericRepository = genericRepository;
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
                throw new Exception("Los campos no pueden ser nulos");
            }

            /*
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
            */        

            var data = _mapper.Map<Restaurant>(branchOfficeRequestDto);
            await _genericRepository.Add(data);
            var restaurantId = data.Id;

            return restaurantId;
        }


    }
}
