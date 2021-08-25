using System;

namespace DTOs.Bookings
{
    public class BookingListResponseDto
    {
        //Booking
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int NumberPeople { get; set; }
        //Booking status
        public string Status  { get; set; }
        //User
        public string FirstName { get; set; }
        public string Mobile { get; set; }
    }
}
