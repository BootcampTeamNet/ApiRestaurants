using DTOs.Dish;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [Route("api/dish-categories")]
    [ApiController]
    public class DishCategoryController : ControllerBase
    {
        private readonly IDishCategoryService _dishCategoryService;

        public DishCategoryController(IDishCategoryService dishCategoryService)
        {
            _dishCategoryService = dishCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DishCategoryResponseDto>>> GetCategoryaAll()
        {
            var categories = await _dishCategoryService.GetAll();
            return Ok(categories);
        }

    }
}
