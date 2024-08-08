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

                return Ok(new { message = "The Hotel Data fectched  Successfully!", hotel });
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
            return BadRequest("Hotel field are Empty");
        }

        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();

        var response = new
        {
            message = "Hotel created successfully!",
            hotel
        };

        return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, response);
    }
    catch (Exception)
    {
        return StatusCode(500, "An error occurred while processing your request.");
    }
}

        [HttpGet("id")]
        public async Task<IActionResult> GetHotelById(int id)
        {

            try
            {
                var hotel = await _context.Hotels.FindAsync(id);
                if (hotel == null)
                {
                    return NotFound("Given Hotel ID is not Found");
                }
                return Ok(new { message = "Given ID is Found", hotel });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateHotel(int Id, [FromBody] Hotel hotel)
        {
            try
            {
                var hotelToUpdate = await _context.Hotels.FindAsync(Id);

                if (hotelToUpdate == null)
                {
                    return NotFound("Given Hotel ID is not Found");
                }

                hotelToUpdate.Name = hotel.Name;
                hotelToUpdate.Price = hotel.Price;
                hotelToUpdate.ImageUrl = hotel.ImageUrl;
                hotelToUpdate.Description = hotel.Description;
                hotelToUpdate.FreeCancellation = hotel.FreeCancellation;
                hotelToUpdate.ReserveNow = hotel.ReserveNow;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Hotel is Updated Successfully!!", hotelToUpdate });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }


        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteHotel(int Id)
        {
            try
            {
                var hotel = await _context.Hotels.FindAsync(Id);
                if (hotel == null)
                {
                    return NotFound("Hotel is not Found");
                }
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Hotel is Deleted Successfully!!", hotel });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchHotel(string name)
        {
            try
            {
                var hotel = await _context.Hotels.Where(h => h.Name!.Contains(name)).ToListAsync();
                if (hotel == null)
                {
                    return NotFound("Hotel Name is not Found");
                }
                return Ok(new { message = "Hotel Name is Found Successfully!!", hotel });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }


        }

    }
}