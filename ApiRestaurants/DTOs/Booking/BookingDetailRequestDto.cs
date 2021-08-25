using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Booking
{
    public class BookingDetailRequestDto
    {
        public int DishId { get; set; }
        public int Quantity { get; set; }
        public string Notes{ get; set; }
    }
}
