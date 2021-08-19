using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DTOs.Dish
{
    public class DishRequestDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La Descripcion es requerida")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public int? CaloriesMinimun { get; set; }
        public int? CaloriesMaximun { get; set; }
        public int? Proteins { get; set; }
        public int? Fats { get; set; }
        public int? Sugars { get; set; }

        [Required(ErrorMessage = "El id de la categoría del plato es requerido")]
        public int DishCategoryId { get; set; }

        [Required(ErrorMessage = "El id del restaurante es requerido")]
        public int RestaurantId { get; set; }

    }
}
