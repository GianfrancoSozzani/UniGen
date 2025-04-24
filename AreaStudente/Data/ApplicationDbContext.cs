using AreaStudente.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AreaStudente.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Studente> Studenti { get; set; } // Example DbSet for the Studente entity
        // Define your DbSets here, for example:
        // public DbSet<Student> Students { get; set; }
    }
}
