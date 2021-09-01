﻿using DTOs.Favourites;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;
        public FavouritesController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        /// <summary>
        /// Add Resturant Favorite by UserId
        /// </summary>
        /// <param name="favouriteRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(FavouriteRequestDto favouriteRequestDto) 
        {
            try
            {
                int response = await _favouriteService.Create(favouriteRequestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get restaurants favourite by userId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("users/{id}")]
        public async Task<ActionResult> GetByUserId(int id)
        {
            try
            {
                var response = await _favouriteService.GetFavouriteList(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
