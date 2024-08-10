using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderMateBackend.DTOs.UserDTOs
{
    public class GetUserDTO
    {
          public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
      
    }
}