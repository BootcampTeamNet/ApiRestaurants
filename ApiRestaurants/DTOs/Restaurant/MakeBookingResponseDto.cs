using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Restaurant
{
    public class MakeBookingResponseDto
    {
        public string Name { get; set; }
        public DateTime OrderDate { get; set; }
        public int NumberPeople { get; set; }
        public DateTime Time { get; set; }
        public int ExpiryTime { get; set; }
    }
}
