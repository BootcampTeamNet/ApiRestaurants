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
