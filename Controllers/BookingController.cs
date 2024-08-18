using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WanderMateBackend.context;
namespace WanderMateBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            try
            {


                return Ok(new { message = "The Booking Data fetched Successfully!" });
            }
            catch (Exception ex)
            {
                 return StatusCode(500, ex.Message);

            }
        }

    }
}