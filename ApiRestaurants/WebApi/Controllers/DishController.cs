using DTOs.Dish;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DishRequestDto dishRequestDto)
        {
            try
            {
                int response = await _dishService.Create(dishRequestDto);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (InaccessibleResourceException ex)
            {
                return StatusCode(401,ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
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
                return StatusCode(404,ex.Message);
            }
            catch (InaccessibleResourceException ex)
            {
                return StatusCode(401,ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPut("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(int id, UpdateStatusDishRequestDto updateStatusDishRequestDto)
        {
            try
            {
                int response = await _dishService.Status(id, updateStatusDishRequestDto.RestaurantId);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404,ex.Message);
            }
            catch (InaccessibleResourceException ex)
            {
                return StatusCode(401,ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("active/restaurant/{id}")]
        public async Task<IActionResult> GetAllActive(int id)
        {
            try
            {
                var activeDishes = await _dishService.GetActiveDishList(id);
                return Ok(activeDishes);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } 

        [HttpGet("Restaurant/{id}")]
        public async Task<IActionResult> GetAllByRestaurantId(int id) {
            try
            {
                var response = await _dishService.GetAllByRestaurantId(id);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404,ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}