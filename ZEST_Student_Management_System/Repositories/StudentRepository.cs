using Microsoft.EntityFrameworkCore;
using ZEST_Student_Management_System.Data;
using ZEST_Student_Management_System.Models;
using ZEST_Student_Management_System.Repositories.Interface;

namespace ZEST_Student_Management_System.Repositories
{
    public class StudentRepository : IStudentRepository
    {
       private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

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
