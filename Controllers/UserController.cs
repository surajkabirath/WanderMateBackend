using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderMateBackend.context;
using WanderMateBackend.DTOs.UserDTOs;
using WanderMateBackend.Models;
using WanderMateBackend.Service;

namespace WanderMateBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
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
                    Role = u.Role


                });
                return Ok(new { message = "The User Data fetched Successfully!", getUserDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("SignUp")]
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
                var role = string.IsNullOrEmpty(createUserDto.Role) ? "User" : createUserDto.Role;
                var HashPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
                // Create a new user
                var newUser = new User
                {
                    Role = createUserDto.Role,
                    Username = createUserDto.Username,
                    Email = createUserDto.Email,
                    Password = HashPassword
                };

                // Add the new user to the database
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
              

                return Ok(new
                {
                    message = "User created successfully.",
                    user = new
                    {
                        newUser.Id,
                        newUser.Username,
                        newUser.Email,
                        newUser.Role
                    }
                });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        // [HttpGet("id")]
        // public async Task<IActionResult> GetUserById(int id)
        // {
        //     try
        //     {
        //         var user = await _context.Users.FindAsync(id);
        //         if (user == null)
        //         {
        //             return NotFound("No User Data Found");
        //         }
        //         var getUserDto = new GetUserByIdDTO
        //         {

        //             Username = user.Username,
        //             Email = user.Email,
        //             Password = user.Password
        //         };
        //         return Ok(new { message = "The User Data fetched Successfully!", getUserDto });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }
        // [HttpPut("id")]
        // public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO updateUserDto)
        // {
        //     try
        //     {
        //         var user = await _context.Users.FindAsync(id);
        //         if (user == null)
        //         {
        //             return NotFound("No User Data Found");
        //         }
        //         user.Username = updateUserDto.Username;
        //         user.Email = updateUserDto.Email;
        //         user.Password = updateUserDto.Password;
        //         await _context.SaveChangesAsync();
        //         return Ok( "User Updated Successfully!" );
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

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