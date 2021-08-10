﻿using System.ComponentModel.DataAnnotations;

namespace DTOs.Users
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
    }
}
