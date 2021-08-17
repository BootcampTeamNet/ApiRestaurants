using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/restaurant-categories")]
    [ApiController]
    public class RestaurantCategoryController : ControllerBase
    {
        private readonly IRestaurantCategoryService _restaurantCategoryService;

        public RestaurantCategoryController(IRestaurantCategoryService restaurantCategoryService)
        {
            _restaurantCategoryService = restaurantCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responseRestaurantCategory = await _restaurantCategoryService.GetAll();
            return Ok(responseRestaurantCategory);
        }

    }
}
