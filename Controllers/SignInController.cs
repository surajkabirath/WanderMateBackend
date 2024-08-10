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
        public async Task<IActionResult> UserLogin([FromBody] SignInDTO signInDTO)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == signInDTO.Username);

                if (user == null)
                {
                    return BadRequest("Username does not exist.");
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(signInDTO.Password, user.Password);
                if (!isPasswordValid)
                {
                    return BadRequest("Password is incorrect.");
                }

                return Ok("User signed in successfully!" );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}