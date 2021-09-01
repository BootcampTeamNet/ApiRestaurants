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
    public class RestaurantCategoryService : IRestaurantCategoryService
    {
        private readonly IGenericRepository<RestaurantCategory> _genericRepository;
        private readonly IMapper _mapper;

        public RestaurantCategoryService( IGenericRepository<RestaurantCategory> genericRepository, IMapper mapper)
        {
    
            _genericRepository = genericRepository;
            _mapper = mapper;

        }

        public async Task<List<RestaurantCategoryResponseDto>> GetAll()
        {
            var responseRestaurantCategory = await _genericRepository.GetAllAsync();
            var response = _mapper.Map<List<RestaurantCategoryResponseDto>>(responseRestaurantCategory);
            return response;
        }
    }
}
