using DTOs.Dish;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Errors;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishCategoryController : ControllerBase
    {
        private readonly IDishCategoryService _dishCategoryService;

        public DishCategoryController(IDishCategoryService dishCategoryService)
        {
            _dishCategoryService = dishCategoryService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<DishCategoryRequestDto>>> GetCategoryaAll()
        {
            var categories = await _dishCategoryService.GetAll();
            return Ok(categories);
        }

    }
}
