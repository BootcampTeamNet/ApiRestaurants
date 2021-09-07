using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Restaurant
{
    public class FilterByDishesRequestDto
    {
        public bool WithLocation { get; set; }
        public double CustomerLatitude { get; set; }
        public double CustomerLongitude { get; set; }
        public List<int> DishCategoriesIdList { get; set; }
    }
}
