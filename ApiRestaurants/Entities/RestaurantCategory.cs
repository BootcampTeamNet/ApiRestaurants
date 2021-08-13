using System;

namespace Entities
{
    public class RestaurantCategory : BaseClass
    {
        public RestaurantCategory()
        {
            IsActive = true;
        }

        public string Name { get; set; }
        public string PathImage { get; set; }
        public bool IsActive { get; set; }

    }
}
