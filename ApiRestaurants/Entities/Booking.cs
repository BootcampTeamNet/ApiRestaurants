using System;

namespace Entities
{
    public class Booking : BaseClass
    {
        public Booking()
        {
            CreationDate = DateTime.Now;
        }

        public DateTime OrderDate { get; set; }
        public int NumberPeople { get; set; }
        public string BookingNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
