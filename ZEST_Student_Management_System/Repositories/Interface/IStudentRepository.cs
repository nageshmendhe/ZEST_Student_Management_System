using ZEST_Student_Management_System.Models;

namespace ZEST_Student_Management_System.Repositories.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Student>> GetAllAsync();
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Student?> GetByIdAsync(int id);

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        Task<Student> AddAsync(Student student);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        Task<Student?> UpdateAsync(Student student);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);
    }
}
