using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderMateBackend.context;
using WanderMateBackend.DTOs.HotelDTOs;
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
                if (hotel == null)
                {
                    return NotFound("No Hotel Data Found");
                }

                var createHotelDTO = hotel.Select(h => new GetHotelDTOs
                {
                    Id = h.Id,
                    Name = h.Name,
                    Price = h.Price,
                    ImageUrl = h.ImageUrl,
                    Description = h.Description,
                    FreeCancellation = h.FreeCancellation,
                    ReserveNow = h.ReserveNow
                });

                return Ok(new { message = "The Hotel Data fetched Successfully!", createHotelDTO });
            }
            catch (Exception ex)
            {


                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDTOs createHotelDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var hotel = new Hotel
                {
                    Name = createHotelDTO.Name,
                    Price = createHotelDTO.Price,
                    ImageUrl = createHotelDTO.ImageUrl,
                    Description = createHotelDTO.Description,
                    FreeCancellation = createHotelDTO.FreeCancellation,
                    ReserveNow = createHotelDTO.ReserveNow
                };


                await _context.Hotels.AddAsync(hotel);
                await _context.SaveChangesAsync();

                var response = new
                {
                    message = "Hotel created successfully!",
                    hotel
                };

                return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetHotelById(int id)
        {

            try
            {
                var hotelById = await _context.Hotels.FindAsync(id);
                if (hotelById == null)
                {
                    return NotFound("Given Hotel ID is not Found");
                }
                var createHotelDTOById = new HotelDTOs
                {
               
                    Name = hotelById.Name,
                    Price = hotelById.Price,
                    ImageUrl = hotelById.ImageUrl,
                    Description = hotelById.Description,
                    FreeCancellation = hotelById.FreeCancellation,
                    ReserveNow = hotelById.ReserveNow
                };

                return Ok(new { message = "Given Id Details:", createHotelDTOById });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateHotel(int Id, [FromBody] HotelDTOs updateHotelDTO)
        {
            try
            {
                var hotelToUpdate = await _context.Hotels.FindAsync(Id);

                if (hotelToUpdate == null)
                {
                    return NotFound("Given Hotel ID is not Found");
                }

                hotelToUpdate.Name = updateHotelDTO.Name;
                hotelToUpdate.Price = updateHotelDTO.Price;
                hotelToUpdate.ImageUrl = updateHotelDTO.ImageUrl;
                hotelToUpdate.Description = updateHotelDTO.Description;
                hotelToUpdate.FreeCancellation = updateHotelDTO.FreeCancellation;
                hotelToUpdate.ReserveNow = updateHotelDTO.ReserveNow;


                _context.Entry(hotelToUpdate).State = EntityState.Modified;


                await _context.SaveChangesAsync();
                return Ok(new { message = "Hotel is Updated Successfully!!", hotelToUpdate });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                return Ok(new { message = "Related Hotel Name are Found Successfully!!", hotel });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

    }
}