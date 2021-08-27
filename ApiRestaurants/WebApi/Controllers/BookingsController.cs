using Microsoft.AspNetCore.Authorization;
using DTOs.Bookings;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// MOBILE - Create a booking
        /// </summary>
        /// <param name="makeBooking"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeBooking(MakeBookingRequestDto makeBooking)
        {
            try
            {
                var response = await _bookingService.MakeBooking(makeBooking);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// WEB - Confirm Booking by BookingId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> ConfirmBooking(int id) 
        {
            try
            {
                int response = await _bookingService.ConfirmById(id);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (EntityBadRequestException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// WEB - Cancel Booking by BookingId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/cancel-byrestaurant")]
        public async Task<IActionResult> CancelBookingByRest(int id)
        {
            try
            {
                int response = await _bookingService.CancelByRestaurant(id);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (EntityBadRequestException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// WEB - Get Bookings by RestaurantId and filter for dates and bookingStatusId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filterBookingRequestDto"></param>
        /// <returns></returns>
        [HttpPost("restaurants/{id}")]
        public async Task<ActionResult> ListByRestaurantId(int id, FilterBookingRequestDto filterBookingRequestDto)
        {
            var response = await _bookingService.ListByRestaurantId(id, filterBookingRequestDto);
            return Ok(response);
        }
    }
}
