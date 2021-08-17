﻿using DTOs.Restaurant;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _restaurantService.GetById(id);
                return Ok(response);
            }
            catch {
                return NotFound(new CodeErrorResponse(404, $"No existe el restaurante de id {id}"));
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Create(RegisterRestaurantRequestDto restaurantRequestDto)
        {
            var response = await _restaurantService.Create(restaurantRequestDto);

            return Ok(response);
        }

        [HttpGet("GetAllByCoordinates")]
        public async Task<IActionResult> GetAllByCoordinates(double customerLatitude, double customerLongitude) 
        {
            var response = await _restaurantService.GetAllByCoordinates(customerLatitude, customerLongitude);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRestaurantUserRequestDto updateRestaurantUserRequestDto)
        {

            var response = await _userRestaurantService.Update(updateRestaurantUserRequestDto);

            return Ok(response);
        }
    }
}
