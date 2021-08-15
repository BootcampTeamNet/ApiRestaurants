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
        private readonly IGenericRepository<Restaurant> _genericRepository;
        private readonly IDishRepository _iDishRepository;
        private readonly IMapper _mapper;

        public BranchOfficeService(
            IGenericRepository<Restaurant> genericRepository,
            IDishRepository _iDishRepository,
            IMapper mapper
            )
        {
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

            var data = _mapper.Map<Restaurant>(branchOfficeRequestDto);
            await _genericRepository.Add(data);
            var restaurantId = data.Id;

            return restaurantId;
        }


    }
}
