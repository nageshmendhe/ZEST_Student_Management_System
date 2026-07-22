using Microsoft.EntityFrameworkCore;
using ZEST_Student_Management_System.Data;
using ZEST_Student_Management_System.Models;
using ZEST_Student_Management_System.Repositories.Interface;

namespace ZEST_Student_Management_System.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ZEST_Student_Management_System.Repositories.Interface.IStudentRepository" />
    public class StudentRepository : IStudentRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        public async Task<Student> AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="student">The student.</param>
        /// <returns></returns>
        public async Task<Student?> UpdateAsync(Student student)
        {
            var result = await _context.Students.FindAsync(student.Id);
            if (result == null)
            {
                return null;
            }
            result.Name = student.Name;
            result.Email = student.Email;
            result.Age = student.Age;
            result.Course = student.Course;

            await _context.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Students.FindAsync(id);
            if (result == null)
            {
                return false;
            }
            _context.Students.Remove(result);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
