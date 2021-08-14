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
        private readonly IUserRestaurantService _userRestaurantService;
        private readonly IMapper _mapper;
        public RestaurantService(IRestaurantRepository restaurantRepository, IUserService userService, IUserRestaurantService userRestaurantService,  IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _userService = userService;
            _userRestaurantService = userRestaurantService;
            _mapper = mapper;

        }
        public async Task<int> Create(RegisterRestaurantRequestDto restaurantRequestDto)
        {
            if (string.IsNullOrEmpty(restaurantRequestDto.Name) || string.IsNullOrEmpty(restaurantRequestDto.Address)
                || string.IsNullOrEmpty(restaurantRequestDto.User?.FirstName) || string.IsNullOrEmpty(restaurantRequestDto.User?.LastName)
                || string.IsNullOrEmpty(restaurantRequestDto.User?.Email) || string.IsNullOrEmpty(restaurantRequestDto.User?.Password)) {
                throw new Exception("Error, todos los campos son requeridos");
            }

            bool exist = await _userService.ExistsUser(restaurantRequestDto.User.Email);
            if (exist)
            {
                throw new Exception($"Ya existe un usuario registrado con el email {restaurantRequestDto.User.Email}");
            }

            return await _userRestaurantService.Add(restaurantRequestDto);
        }

        public async Task<RestaurantResponseDto> GetById(int id)
        {   
            var restaurant = await _restaurantRepository.GetById(id);
            if(restaurant == null) 
            { 
                throw new ArgumentNullException("NotFound");
            }
            
            var restaurantResponseDto = _mapper.Map<RestaurantResponseDto>(restaurant);
            return restaurantResponseDto;
        }   
    }
}
