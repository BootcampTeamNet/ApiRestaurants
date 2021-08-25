using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Restaurant
{
    public class MakeBookingResponseDto
    {
        public MakeBookingResponseDto(int id, DateTime date, int people, int? maxTime)
        {
            this.UserId = id;
            this.OrderDate = date;
            this.NumberPeople = people;
            this.TimeMaxCancelBooking = maxTime;
        }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int NumberPeople { get; set; }
        public int? TimeMaxCancelBooking { get; set; }
    }
}
