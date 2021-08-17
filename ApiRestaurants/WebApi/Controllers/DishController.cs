using DTOs.Dish;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations.Dishes;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Errors;

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
        public async Task<IActionResult> Create([FromForm] DishRequestDto dishRequestDto)
        {
            return Ok(await _dishService.Create(dishRequestDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] DishRequestDto dishRequestDto)
        {
            try
            {
                int response = await _dishService.Update(id, dishRequestDto);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {

                return StatusCode(404, ex);
            }
            catch (InaccessibleResourceException ex)
            {

                return StatusCode(401, ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var responseDish = await _dishService.GetById(id);

                return Ok(responseDish);
            }
            catch(Exception)
            {
                return NotFound("El id ingresado no coincide con algun plato registrado.");
            }
        } 

        [HttpGet("restaurants/{id}")]
        public async Task<IActionResult> GetDishesByResutaurants(int id) {
            try 
            {
                var response = await _dishService.GetListByIdRestaurant(id);
                return Ok(response);
            }
            catch 
            { 
                return NotFound("No existe el restaurante de id {id} o platos asociados a ese restaurante");
            }
        }
    }
}