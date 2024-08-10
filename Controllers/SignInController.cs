using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderMateBackend.context;
using WanderMateBackend.DTOs.UserDTOs;

namespace WanderMateBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignInController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SignInController(ApplicationDbContext context)
        {
            _context = context;



        }
        [HttpPost]
        public async Task<IActionResult> SignInUser([FromBody] SignInDTO signInDto)
        {
            try
            {
                // Search for the user by email
                var searchUsername = await _context.Users.SingleOrDefaultAsync(u => u.Username == signInDto.Username);

                if (searchUsername == null)
                {
                    return BadRequest("Email does not exist!!");
                }

                // Verify the password using BCrypt
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(signInDto.Password, searchUsername.Password);
                if (!isPasswordValid)
                {
                    return BadRequest("Password is incorrect!!");
                }

                return Ok(new { message = "User Signed In Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}