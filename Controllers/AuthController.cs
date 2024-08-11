using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WanderMateBackend.context;
using WanderMateBackend.DTOs.UserDTOs;
using WanderMateBackend.Models;
using WanderMateBackend.Service;

namespace WanderMateBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        // private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
            // _tokenService = tokenService;



        }
        [HttpPost("SignIn")]
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

                // Generate JWT token
                // var token = _tokenService.GenerateToken(user);

                // Store token and user data in the session
                // HttpContext.Session.SetString("AuthToken", token);
                // HttpContext.Session.SetString("UserName", user.Username ?? string.Empty);
                // HttpContext.Session.SetString("Email", user.Email ?? string.Empty);

                // return Ok(new { Message = "User signed in successfully!", Token = token });

                return Ok("User signed in successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }
        //    [HttpPost ("logout")]
        // public IActionResult Logout()
        // {
        //     try
        //     {
        //         // Retrieve user information from the session before clearing it
        //         string username = HttpContext.Session.GetString("UserName") ?? string.Empty;

        //         if (string.IsNullOrEmpty(username))
        //         {
        //             return BadRequest("No user is currently logged in.");
        //         }

        //         // Clear the session data
        //         HttpContext.Session.Clear();

        //         // Optionally, you can log or handle the logout event as needed
        //         return Ok(new { Message = $"{username} has logged out successfully!" });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }


    }
}