using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderMateBackend.context;
using WanderMateBackend.DTOs.UserDTOs;
using WanderMateBackend.Models;

namespace WanderMateBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignUpController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SignUpController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var user = await _context.Users.ToListAsync();
                if (user == null)
                {
                    return NotFound("No User Data Found");
                }
                var getUserDto = user.Select(u => new GetUserDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Password = u.Password

                });
                return Ok(new { message = "The User Data fetched Successfully!", getUserDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO createUserDto)
        {
            try
            {
                var searchUserEmail = await _context.Users.SingleOrDefaultAsync(u => u.Email == createUserDto.Email);

                if (searchUserEmail != null)
                {
                    return BadRequest("Email already exists!!");
                }

                if (createUserDto.Password != createUserDto.ConfirmPassword)
                {
                    return BadRequest("Passwords do not match!!");
                }

                var newUser = new User
                {
                    Username = createUserDto.Username,
                    Email = createUserDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password)
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                // return StatusCode(200, "User Created Successfully!!");
                return Ok(new { message = "User Created Successfully!!", newUser });
            }

            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound("No User Data Found");
                }
                var getUserDto = new GetUserByIdDTO
                {

                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password
                };
                return Ok(new { message = "The User Data fetched Successfully!", getUserDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }


}