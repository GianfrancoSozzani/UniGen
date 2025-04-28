using AreaDocente.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Materiali> materiali { get; set; }

    }
}
