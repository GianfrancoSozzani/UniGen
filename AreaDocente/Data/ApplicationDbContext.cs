using AreaDocente.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AreaDocente.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<MVCMateriali> materiali { get; set; }
        public DbSet<MVCEsame> esami { get; set; }
        public DbSet<MVCAPPELLO> appelli { get; set; }
        public DbSet<MVCDOCENTE> docenti { get; set; }
        public DbSet<MVCPROVA> prove { get; set; }
        public DbSet<MVCTest_DA> test_DA { get; set; }
        public DbSet<MVCTest_DC> test_DC { get; set; }
        public DbSet<MVCLezioni> lezioni { get; set; }
        public DbSet<MVCStudente> studenti { get; set; }
        public DbSet<MVCLibretto> libretti { get; set; }
        public DbSet<MVCValutazione> valutazioni { get; set; }
        public DbSet<Comunicazione> Comunicazioni { get; set; }
        public DbSet<PianoStudioPersonale> PianiStudioPersonali { get; set; }

    }
}
