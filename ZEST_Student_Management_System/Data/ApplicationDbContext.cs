using Microsoft.EntityFrameworkCore;
using ZEST_Student_Management_System.Models;

namespace ZEST_Student_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
