using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WanderMateBackend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Role {get; set;}
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}