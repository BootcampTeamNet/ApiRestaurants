using DataAccess.Interfaces;
using DTOs.Dish;
using Entities;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations.Dishes
{
    public class DishService : IDishService
    {
        private readonly IGenericRepository<Dish> _genericRepository;
        public DishService(IGenericRepository<Dish> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public Task<List<DishRequestDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }
        public Task<DishRequestDto> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Create(DishRequestDto dishRequestDto)
        {
            throw new System.NotImplementedException();
        }


        public Task<int> Update(DishRequestDto dishRequestDto)
        {
            throw new System.NotImplementedException();
        }

    }
}
