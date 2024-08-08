using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotel = await _context.Hotels.ToListAsync();

                return Ok(new { message = "The hotel is deleted Successfully!", hotel });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");

            }


        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            try
            {
                if (hotel == null)
                {
                    return BadRequest();
                }
                var newHotel = await _context.Hotels.AddAsync(hotel);
                await _context.SaveChangesAsync();
                return Ok(new{message="hotel created successfully!",newHotel});
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetHotelById(int Id)
        {
            var hotel = await _context.Hotels.FindAsync(Id);
            return Ok(hotel);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateHotel(int Id, [FromBody] Hotel hotel)
        {
            var hotelToUpdate = await _context.Hotels.FindAsync(Id);

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

            await _context.SaveChangesAsync();
            return Ok(hotelToUpdate);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteHotel(int Id)
        {
            var hotel = await _context.Hotels.FindAsync(Id);
            if (hotel == null)
            {
                return NotFound();
            }
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return Ok(hotel);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchHotel(string name)
        {
            var hotel = await _context.Hotels.Where(h => h.Name!.Contains(name)).ToListAsync();
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

    }
}