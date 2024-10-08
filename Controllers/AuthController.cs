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
using Microsoft.AspNetCore.Authorization;

namespace WanderMateBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;



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
                var token = _tokenService.GenerateToken(user);
                var response = new
                {

                    Token = token,
                    Role = user.Role, // Ensure 'Role' is part of the user object
                    ExpiresIn = DateTime.Now.AddMinutes(30)
                };


                return Ok(new { Message = "User signed in successfully!", response = response });

                // return Ok("User signed in successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }
        [Authorize]
        [HttpGet("verify-token")]
        public IActionResult VerifyToken()
        {

            return Ok("Token is Verified");

        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            try
            {

                return Ok(new { Message = "User logged out successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}