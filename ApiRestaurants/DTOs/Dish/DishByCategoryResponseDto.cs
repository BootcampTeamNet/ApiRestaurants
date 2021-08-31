using System.Collections.Generic;

namespace DTOs.Dish
{
    public class DishByCategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DishDto> Dishes { get; set; }
    }
}
