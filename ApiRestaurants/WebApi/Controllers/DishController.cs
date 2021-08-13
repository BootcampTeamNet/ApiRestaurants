﻿using DTOs.Dish;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        public Task<List<DishRequestDto>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public Task<int> Create(DishRequestDto dishRequestDto)
        {
            return _dishService.Create(dishRequestDto);
        }

        [HttpPut("{id}")]
        public Task<int> Update(int id, [FromForm]DishRequestDto dishRequestDto)
        {
            return _dishService.Update(dishRequestDto, id);
        }
    }
}
