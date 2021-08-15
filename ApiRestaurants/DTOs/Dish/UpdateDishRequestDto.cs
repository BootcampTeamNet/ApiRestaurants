using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Dish
{
    public class UpdateDishRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public int? CaloriesMinimun { get; set; }
        public int? CaloriesMaximun { get; set; }
        public int? Proteins { get; set; }
        public int? Fats { get; set; }
        public int? Sugars { get; set; }
        public int DishCategoryId { get; set; }
    }
}
