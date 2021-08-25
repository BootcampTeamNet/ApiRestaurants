using System;
using System.Collections.Generic;

namespace DTOs.Bookings
{
    public class MakeBookingRequestDto
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime OrderDate { get; set; }
        public int NumberPeople { get; set; }
        public List<BookingDetailRequestDto> DishesList { get; set; }
    }   
}
