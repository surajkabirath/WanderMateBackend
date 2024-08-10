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
        // Check if the email already exists
        var emailExists = await _context.Users.SingleOrDefaultAsync(u => u.Email == createUserDto.Email);
        if (emailExists != null)
        {
            return BadRequest("Email already exists.");
        }

        // Check if the username already exists
        var usernameExists = await _context.Users.SingleOrDefaultAsync(u => u.Username == createUserDto.Username);
        if (usernameExists != null)
        {
            return BadRequest("Username already exists.");
        }

        // Check if passwords match
        if (createUserDto.Password != createUserDto.ConfirmPassword)
        {
            return BadRequest("Passwords do not match.");
        }

        // Create a new user
        var newUser = new User
        {
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password)
        };

        // Add the new user to the database
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User created successfully." });
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

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound("No User Data Found");
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(new { message = "User Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }




}