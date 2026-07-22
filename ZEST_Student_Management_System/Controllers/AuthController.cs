using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Services.Interface;

namespace ZEST_Student_Management_System.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// The token service
        /// </summary>
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Logins the specified login dto.
        /// </summary>
        /// <param name="loginDto">The login dto.</param>
        /// <returns></returns>
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
