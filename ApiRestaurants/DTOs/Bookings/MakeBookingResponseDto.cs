using System;

namespace DTOs.Bookings
{
    public class MakeBookingResponseDto
    {
        public MakeBookingResponseDto(string userName, DateTime date, int people, int? maxTime)
        {
            UserName = userName;
            OrderDate = date;
            NumberPeople = people;
            TimeMaxCancelBooking = maxTime;
        }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public int NumberPeople { get; set; }
        public int? TimeMaxCancelBooking { get; set; }
    }
}
