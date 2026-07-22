using System.ComponentModel.DataAnnotations;

namespace ZEST_Student_Management_System.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateStudentDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        [Range(1, 120)]
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the course.
        /// </summary>
        /// <value>
        /// The course.
        /// </value>
        [Required]
        [StringLength(100)]
        public string Course { get; set; } = string.Empty;

    }
}
 