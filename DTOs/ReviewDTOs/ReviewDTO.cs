using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderMateBackend.DTOs.ReviewDTOs
{
    public class ReviewDTO
    {
        public int Rating { get; set; } 

        public string? ReviewText { get; set; } 

        public int HotelId { get; set; }
    }
}