using ZEST_Student_Management_System.DTOs;

namespace ZEST_Student_Management_System.Services.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        LoginResponseDto GenerateToken(string username);
    }
}
