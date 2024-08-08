using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderMateBackend.context;

namespace WanderMateBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
           try{
            var reviews = await _context.Reviews.ToListAsync();
            if(reviews == null){
                return NotFound("No Review Data Found");
            }
            return Ok(reviews);

           }catch(Exception ex){
            return StatusCode(500,ex.Message);
           }
        }
    }
}