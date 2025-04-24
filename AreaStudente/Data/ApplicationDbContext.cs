using AreaStudente.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AreaStudente.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Soggetto> Studenti { get; set; }
        public DbSet<Comunicazione> Comunicazioni { get; set; }
    }
}
