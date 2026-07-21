namespace ZEST_Student_Management_System.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Course { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
