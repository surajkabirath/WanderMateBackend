using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderMateBackend.context;
using WanderMateBackend.Models;

namespace WanderMateBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var reviews = await _context.Reviews
                                            .Include(r => r.Hotel) // Include related Hotel data
                                            .ToListAsync();

                return Ok(reviews); // Return 200 OK with the list of reviews
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return 500 Internal Server Error with the exception message
            }
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            try
            {
                var review = await _context.Reviews
                                           .Include(r => r.Hotel) // Include related Hotel data
                                           .FirstOrDefaultAsync(r => r.ReviewId == id);

                if (review == null)
                {
                    return NotFound(new { message = "Review not found" }); // Return 404 Not Found if the review doesn't exist
                }

                return Ok(review); // Return 200 OK with the review data
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return 500 Internal Server Error with the exception message
            }
        }

        // POST: api/Review
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Data");
                }

                review.CreatedOn = DateTime.Now;
                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Review Created Successfully!", review });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Review/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review review)
        {
            try
            {
                if (id != review.ReviewId)
                {
                    return BadRequest("Review ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Data");
                }

                var existingReview = await _context.Reviews.FindAsync(id);
                if (existingReview == null)
                {
                    return NotFound(new { message = "Review not found" });
                }

                // Update properties
                existingReview.Rating = review.Rating;
                existingReview.ReviewText = review.ReviewText;
                existingReview.CreatedOn = DateTime.Now; // Update timestamp

                _context.Entry(existingReview).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Review Updated Successfully!", existingReview });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var review = await _context.Reviews.FindAsync(id);
                if (review == null)
                {
                    return NotFound(new { message = "Review not found" });
                }

                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Review Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
