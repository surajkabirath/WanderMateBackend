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

                });
                return Ok(new { message = "The User Data fetched Successfully!", getUserDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}