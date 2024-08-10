using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderMateBackend.DTOs.UserDTOs
{
    public class UserDTO
    {
        
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}