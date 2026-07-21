using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZEST_Student_Management_System.DTOs;
using ZEST_Student_Management_System.Services.Interface;

namespace ZEST_Student_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound(new
                {
                    Message = $"Student with ID {id} not found."
                });
            }
            return Ok(student);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentDto studentDto)
        {
            var student = await _studentService.AddStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("Update/{id}")]
        public async Task <IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto studentDto)
        {
            var student = await _studentService.UpdateStudentAsync(id, studentDto);

            if(student == null)
            {
                return NotFound(new
                {
                    Message = $"Student with ID {id} not found."
                });
            }
            return Ok(student);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.DeleteStudentAsync(id);

            if(!student)
            {
                return NotFound(new
                {
                    Message = $"Student with ID {id} not found."
                });
            }
            return Ok(new
            {
                Message = $"Student with ID {id} deleted successfully."
            });
        }
    }
}
