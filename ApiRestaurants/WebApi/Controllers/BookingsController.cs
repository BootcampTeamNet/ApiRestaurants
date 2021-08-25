using DTOs.Bookings;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;


        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("bestselling-dishes/restaurant/{id}")]
        public async Task<IActionResult> GetBestBookingList(int id)
        {
            List<BestSellingDishesResponseDto> bestSellingDishes =  await _bookingService.GetBestBookingList(id);
            return Ok(bestSellingDishes);
        }
    }
}
