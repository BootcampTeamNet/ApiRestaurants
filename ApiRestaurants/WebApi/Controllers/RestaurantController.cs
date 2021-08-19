using DTOs.Restaurant;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Exceptions;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IUserRestaurantService _userRestaurantService;

        public RestaurantController(IRestaurantService restaurantService, IUserRestaurantService userRestaurantService)
        {
            _restaurantService = restaurantService;
            _userRestaurantService = userRestaurantService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create(RegisterRestaurantRequestDto restaurantRequestDto)
        {
            try
            {
                var response = await _restaurantService.Create(restaurantRequestDto);
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

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRestaurantUserRequestDto updateRestaurantUserRequestDto)
        {
            try
            {
                var response = await _userRestaurantService.Update(updateRestaurantUserRequestDto);

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

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _restaurantService.GetById(id);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("by-coordinates")]
        public async Task<IActionResult> GetAllByCoordinates(double customerLatitude, double customerLongitude) 
        {
            var response = await _restaurantService.GetAllByCoordinates(customerLatitude, customerLongitude);
            return Ok(response);
        }


    }
}
