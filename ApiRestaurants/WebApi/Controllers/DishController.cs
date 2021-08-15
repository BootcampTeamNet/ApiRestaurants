using DTOs.Dish;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        public Task<List<DishRequestDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public Task<int> Create(DishRequestDto dishRequestDto)
        {
            return _dishService.Create(dishRequestDto);
        }

        [HttpPut("{id}")]
        public Task<int> Update(DishRequestDto dishRequestDto, int id)
        {
            return _dishService.Update(dishRequestDto, id);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var responseDish = await _dishService.GetById(id);
                return Ok(responseDish);
            }
            catch
            {
                return NotFound("El id ingresado no coincide con algun plato registrado.");
            }

            
        }
    }
}
