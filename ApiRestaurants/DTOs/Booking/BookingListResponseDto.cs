using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Booking
{
    public class BookingListResponseDto
    {
        //Booking
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int NumberPeople { get; set; }
        //Booking status
        public bool IsActive { get; set; }
        //User
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
    }
}
