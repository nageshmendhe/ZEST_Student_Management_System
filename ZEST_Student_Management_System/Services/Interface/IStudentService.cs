using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Models;

namespace ZEST_Student_Management_System.Services.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Gets all students asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Student>> GetAllStudentsAsync();

        /// <summary>
        /// Gets the student by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Student?> GetStudentByIdAsync(int id);

        /// <summary>
        /// Adds the student asynchronous.
        /// </summary>
        /// <param name="studentDto">The student dto.</param>
        /// <returns></returns>
        Task<Student> AddStudentAsync(CreateStudentDto studentDto);

        /// <summary>
        /// Updates the student asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="studentDto">The student dto.</param>
        /// <returns></returns>
        Task<Student?> UpdateStudentAsync(int id, UpdateStudentDto studentDto);

        /// <summary>
        /// Deletes the student asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteStudentAsync(int id);
    }
}
 