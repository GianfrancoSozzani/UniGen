using Microsoft.EntityFrameworkCore;

namespace AreaPubblica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Studente> Studenti { get; set; }

    }
}
