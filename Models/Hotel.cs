using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WanderMateBackend.Models
{
    
    public class Hotel
    
    {
        
        [Key]
        public int Id {get; set;}
        public string? Name {get; set;}
        public float Price {get; set;}
        public List<string> ImageUrl {get; set;} = new List<string> ();
        public string? Description {get; set;}
        public bool FreeCancellation {get; set;} = false;
        public bool ReserveNow {get ; set;} = false;


 // Navigation property for related reviews
        public List<Review> Reviews {get; set;} = new List<Review>();


    }
}