using DTOs.Restaurant;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Create(RestaurantRequestDto restaurantRequestDto)
        {
            var response = await _restaurantService.Create(restaurantRequestDto);

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var responseRestaurantCategory = await _restaurantService.GetList();
            return Ok(responseRestaurantCategory);
        }

    }
}
