using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WanderMateBackend.context;
using WanderMateBackend.Models;
namespace WanderMateBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class hotelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public hotelController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetHotels()
        {
            var hotel = _context.Hotels.ToList();
            return Ok(hotel);
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            var newHotel = _context.Hotels.Add(hotel);
            _context.SaveChanges();
            return Ok(newHotel.Entity);
        }

        [HttpGet("Id")]
        public IActionResult GetHotelById(int Id)
        {
            var hotel = _context.Hotels.Find(Id);
            return Ok(hotel);
        }

        [HttpPut("id")]
        public IActionResult UpdateHotel(int Id, [FromBody] Hotel hotel)
        {
            var hotelToUpdate = _context.Hotels.Find(Id);

            if (hotelToUpdate == null)
            {
                return NotFound();
            }

            hotelToUpdate.Name = hotel.Name;
            hotelToUpdate.Price = hotel.Price;
            hotelToUpdate.ImageUrl = hotel.ImageUrl;
            hotelToUpdate.Description = hotel.Description;
            hotelToUpdate.FreeCancellation = hotel.FreeCancellation;
            hotelToUpdate.ReserveNow = hotel.ReserveNow;

            _context.SaveChanges();
            return Ok(hotelToUpdate);
        }

        [HttpDelete("id")]
        public IActionResult DeleteHotel(int Id)
        {
            var hotel = _context.Hotels.Find(Id);
            if (hotel == null)
            {
                return NotFound();
            }
            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
            return Ok(hotel);
        }
        [HttpGet("search")]
        public IActionResult SearchHotel(string name)
        {
            var hotel = _context.Hotels.Where(h => h.Name!.Contains(name)).ToList();
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

    }
}