using DTOs.Restaurant;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IPasswordService _passwordService;

        public RestaurantController(IRestaurantService restaurantService, IPasswordService passwordService)
        {
            _restaurantService = restaurantService;
            _passwordService = passwordService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _restaurantService.GetById(id);
                return Ok(response);
            }
            catch {
                return NotFound(new CodeErrorResponse(400, $"No existe el restaurante de id {id}"));
            }

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Create(RegisterRestaurantRequestDto restaurantRequestDto)
        {
            var response = await _restaurantService.Create(restaurantRequestDto);

            return Ok(response);
        }

        /*
        [HttpGet("ListCategory")]
        public async Task<IActionResult> GetList()
        {
            var responseRestaurantCategory = await _restaurantService.GetList();
            return Ok(responseRestaurantCategory);
        }
        */

    }
}
