using DTOs.Dish;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/dishes")]
    [ApiController]
    [Authorize]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        /// <summary>
        /// Create a new dish
        /// </summary>
        /// <param name="dishRequestDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update data dish by dishId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dishRequestDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Active or inactive dish by dishId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateStatusDishRequestDto"></param>
        /// <returns></returns>
        [HttpPut("{id}/change-status")]
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


        /// <summary>
        /// Get data about dish by dishId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get active and inactive dishes by restaurantId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("restaurants/{id}")]
        public async Task<IActionResult> GetAllByRestaurantId(int id) 
        {
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

        /// <summary>
        /// MOBILE - Get active dishes by restaurantId and group by dishCategoryId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("by-categories/restaurants/{id}")]
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
    }
}