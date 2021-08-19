using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Restaurant
{
    public class BranchOfficeRequestDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public int MainBranchId { get; set; }
        public RegisterUserRequestDto User { get; set; }
    }
}
