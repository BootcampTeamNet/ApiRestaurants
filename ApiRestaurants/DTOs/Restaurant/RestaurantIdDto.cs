using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Restaurant
{
    public class RestaurantIdDto
    {   
        public string Name { get; set; }
        public string Address { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string Phone { get; set; }
        public string PathImage { get; set; }
        public string Email { get; set; }
    }
}
