using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderMateBackend.DTOs.UserDTOs
{
    public class GetUserByIdDTO
    {
         public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}