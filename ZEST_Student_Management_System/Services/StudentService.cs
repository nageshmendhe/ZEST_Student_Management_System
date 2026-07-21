using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Models;
using ZEST_Student_Management_System.Repositories.Interface;
using ZEST_Student_Management_System.Services.Interface;

namespace ZEST_Student_Management_System.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<Student> AddStudentAsync(CreateStudentDto studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
                Email = studentDto.Email,
                Age = studentDto.Age,
                Course = studentDto.Course,
                CreateDate = DateTime.UtcNow
            };

            return await _studentRepository.AddAsync(student);
        }

        public async Task<Student?> UpdateStudentAsync(int id,UpdateStudentDto studentDto)
        {
            var student = new Student
            {
                Id = id,
                Name = studentDto.Name,
                Email = studentDto.Email,
                Age = studentDto.Age,
                Course = studentDto.Course
            };

            return await _studentRepository.UpdateAsync(student);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteAsync(id);
        }
    }
}
