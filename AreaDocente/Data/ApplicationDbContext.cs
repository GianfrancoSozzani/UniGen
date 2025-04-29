using AreaDocente.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Materiali> materiali { get; set; }
        public DbSet<MVCLezioni> lezioni { get; set; }

    }
}
