using System.ComponentModel.DataAnnotations;

namespace ZEST_Student_Management_System.DTOs
{
    public class CreateStudentDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [StringLength(100)]
        public string Course { get; set; } = string.Empty;

    }
}
 