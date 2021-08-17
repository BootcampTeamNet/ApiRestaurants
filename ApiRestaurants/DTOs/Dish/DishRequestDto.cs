using Microsoft.AspNetCore.Http;

namespace DTOs.Dish
{
    public class DishRequestDto
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
        public int RestaurantId { get; set; }

    }
}
