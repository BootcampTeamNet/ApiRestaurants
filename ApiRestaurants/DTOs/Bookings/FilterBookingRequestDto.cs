using System;
using System.Collections.Generic;

namespace DTOs.Bookings
{
    public class FilterBookingRequestDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<int> BookingStatusId { get; set; }
    }
}
