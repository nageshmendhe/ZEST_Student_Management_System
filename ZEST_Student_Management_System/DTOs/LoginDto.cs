using System.ComponentModel.DataAnnotations;

namespace ZEST_Student_Management_System.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
