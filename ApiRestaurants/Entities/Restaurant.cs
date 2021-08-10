using DTOs.Users;
using System;

namespace Entities
{
    public class Restaurant : BaseClass
    {
        public Restaurant()
        {
            CreationDate = DateTime.Now;
            IsActive = true;
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string Phone { get; set; }
        public string PathImage { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
