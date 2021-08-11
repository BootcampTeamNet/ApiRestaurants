using System;

namespace Entities
{
    public class Dish : BaseClass
    {
        public Dish() 
        {
            CreationDate = DateTime.Now;
            IsActive = true;
            Price = 0.00M;
            CaloriesMinimun = 0;
            CaloriesMaximun = 0;
            Proteins = 0;
            Fats = 0;
            Sugars = 0;

        }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public string PathImage { get; set; }
        public int CaloriesMinimun { get; set; }
        public int CaloriesMaximun { get; set; }
        public int Proteins { get; set; }
        public int Fats { get; set; }
        public int Sugars { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public int DishCategoryId { get; set; }
        public DishCategory DishCategory { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

    }
}
