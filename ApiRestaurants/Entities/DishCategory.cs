namespace Entities
{
    public class DishCategory:BaseClass
    {
        public DishCategory()
        {
            IsActive = true;
        }

        public string Name { get; set; }
        public string PathImage { get; set; }
        public bool IsActive { get; set; }
    }
}
