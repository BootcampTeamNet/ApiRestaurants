using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Dish
{
    public class DishResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PathImage { get; set; }
        public int CaloriesMinimun { get; set; }
        public int CaloriesMaximun { get; set; }
        public int Proteins { get; set; }
        public int Fats { get; set; }
        public int Sugars { get; set; }
        public bool IsActive { get; set; }
        public int DishCategoryId { get; set; }
        public int RestaurantId { get; set; }
    }
}
