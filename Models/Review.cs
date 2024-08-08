using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WanderMateBackend.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int Rating{get;set;}
        public string? ReviewText  { get; set; }
        public DateTime CreatedOn  { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

    }
}