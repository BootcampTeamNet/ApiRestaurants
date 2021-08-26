using System;

namespace Entities
{
    public class BookingCustomer
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int NumberPeople { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string Mobile { get; set; }
    }
}
