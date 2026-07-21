using ZEST_Student_Management_System.DTOs;

namespace ZEST_Student_Management_System.Services.Interface
{
    public interface ITokenService
    {
        LoginResponseDto GenerateToken(string username);
    }
}
