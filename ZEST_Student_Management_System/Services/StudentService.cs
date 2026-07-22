using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Models;
using ZEST_Student_Management_System.Repositories.Interface;
using ZEST_Student_Management_System.Services.Interface;

namespace ZEST_Student_Management_System.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ZEST_Student_Management_System.Services.Interface.IStudentService" />
    public class StudentService : IStudentService
    {
        /// <summary>
        /// The student repository
        /// </summary>
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentService"/> class.
        /// </summary>
        /// <param name="studentRepository">The student repository.</param>
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Gets all students asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets the student by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Adds the student asynchronous.
        /// </summary>
        /// <param name="studentDto">The student dto.</param>
        /// <returns></returns>
        public async Task<Student> AddStudentAsync(CreateStudentDto studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
                Email = studentDto.Email,
                Age = studentDto.Age,
                Course = studentDto.Course,
                CreatedDate = DateTime.UtcNow
            };

            return await _studentRepository.AddAsync(student);
        }

        /// <summary>
        /// Updates the student asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="studentDto">The student dto.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes the student asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteAsync(id);
        }
    }
}
