using DTOs.Restaurant;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
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
        
        [HttpGet{"id"}]
        public async Task<ActionResult> GetById(int id) 
        {
        var response = await _restaurantService.GetById(int id);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Create(RestaurantRequestDto restaurantRequestDto)
        {
            var response = await _restaurantService.Create(restaurantRequestDto);

            return Ok(response);
        }

    }
}
