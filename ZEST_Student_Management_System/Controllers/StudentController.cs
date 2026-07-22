using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Services.Interface;

namespace ZEST_Student_Management_System.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        /// <summary>
        /// The student service
        /// </summary>
        private readonly IStudentService _studentService;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<StudentController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentController"/> class.
        /// </summary>
        /// <param name="studentService">The student service.</param>
        /// <param name="logger">The logger.</param>
        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStudents()
        {
            _logger.LogInformation("Fetching all students.");

            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        /// <summary>
        /// Gets the student by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            _logger.LogInformation("Fetching student with Id {StudentId}.", id);

            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                _logger.LogWarning("Student with Id {StudentId} was not found.", id);
                return NotFound(new
                {
                    Message = $"Student with ID {id} not found."
                });
            }
            return Ok(student);
        }

        /// <summary>
        /// Adds the student.
        /// </summary>
        /// <param name="studentDto">The student dto.</param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentDto studentDto)
        {

            var student = await _studentService.AddStudentAsync(studentDto);
            _logger.LogInformation("Student created successfully with Id {StudentId}.", student.Id);

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        /// <summary>
        /// Updates the student.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="studentDto">The student dto.</param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public async Task <IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto studentDto)
        {
            var student = await _studentService.UpdateStudentAsync(id, studentDto);

            if(student == null)
            {
                _logger.LogWarning("Update failed. Student with Id {StudentId} was not found.", id);
                return NotFound(new
                {
                    Message = $"Student with ID {id} not found."
                });
            }
            _logger.LogInformation("Student with Id {StudentId} updated successfully.", id);
            return Ok(student);
        }

        /// <summary>
        /// Deletes the student.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);

            if (!result)
            {
                _logger.LogWarning(
                    "Delete failed. Student with Id {StudentId} was not found.",
                    id);

                return NotFound(new
                {
                    message = $"Student with Id {id} not found."
                });
            }

            _logger.LogInformation(
                "Student with Id {StudentId} deleted successfully.",
                id);

            return NoContent();
        }
    }
}
