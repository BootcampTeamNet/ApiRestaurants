using System;

namespace Entities
{
    public class User : BaseClass
    {
        public User() 
        {
            CreateDate = DateTime.Now;
            IsActive = true;
        }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; } 
    }
}