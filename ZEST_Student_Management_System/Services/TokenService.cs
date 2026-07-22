using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Services.Interface;

namespace ZEST_Student_Management_System.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ZEST_Student_Management_System.Services.Interface.ITokenService" />
    public class TokenService : ITokenService
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">JWT key is not configured.</exception>
        public LoginResponseDto GenerateToken(string username)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expireMinutes = int.TryParse(_configuration["Jwt:ExpireMinutes"], out var minutes) ? minutes : 60;

            var expiration = DateTime.UtcNow.AddMinutes(expireMinutes);

            var claims = new[]
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    username
                    ),
                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()
                    ),
                new Claim(
                    ClaimTypes.Name,
                    username
                    )
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(token),

                Expiration = expiration
            };

        }
    }
}
