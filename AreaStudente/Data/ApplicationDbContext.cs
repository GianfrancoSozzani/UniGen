using AreaStudente.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AreaStudente.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Studente> Studenti { get; set; }
    }
}
