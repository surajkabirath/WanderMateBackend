using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderMateBackend.DTOs.HotelDTOs
{
    public class CreateHotelDTOs
    {
         public string? Name {get; set;}
        public float Price {get; set;}
        public List<string> ImageUrl {get; set;} = new List<string> ();
        public string? Description {get; set;}
        public bool FreeCancellation {get; set;} = false;
        public bool ReserveNow {get ; set;} = false;
    }
}