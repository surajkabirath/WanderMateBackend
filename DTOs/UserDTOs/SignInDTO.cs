using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderMateBackend.DTOs.UserDTOs
{
    public class SignInDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}