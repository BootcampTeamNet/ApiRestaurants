using DTOs.Bookings;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("bestselling-dishes/restaurant/{id}")]
        public async Task<IActionResult> GetBestBookingList(int id)
        {
            List<BestSellingDishesResponseDto> bestSellingDishes = await _dashboardService.GetBestBookingList(id);
            return Ok(bestSellingDishes);
        }
    }
}
