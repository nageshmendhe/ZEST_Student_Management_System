using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Services.Interface;

namespace ZEST_Student_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto.Username != "admin" ||
                loginDto.Password != "Admin@123")
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password."
                });
            }

            var response =
                _tokenService.GenerateToken(loginDto.Username);

            return Ok(response);
        }
    }
}
