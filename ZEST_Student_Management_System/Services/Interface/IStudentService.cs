using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Models;

namespace ZEST_Student_Management_System.Services.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();

        Task<Student?> GetStudentByIdAsync(int id);

        Task<Student> AddStudentAsync(CreateStudentDto studentDto);

        Task<Student?> UpdateStudentAsync(int id, UpdateStudentDto studentDto);

        Task<bool> DeleteStudentAsync(int id);
    }
}
 