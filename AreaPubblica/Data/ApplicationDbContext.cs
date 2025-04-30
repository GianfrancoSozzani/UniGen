using Microsoft.EntityFrameworkCore;
using AreaPubblica.Models.Entities;

namespace AreaPubblica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Studente> Studenti { get; set; }
        public DbSet<Operatore> Operatori { get; set; }
        public DbSet<Esame> Esami { get; set; }
        public DbSet<PianoStudio> PianoStudi { get; set; }
        public DbSet<Corso> Corsi { get; set; }
        public DbSet<Docente> Docenti { get; set; }
        public DbSet<Facolta> Facolta { get; set; }
        public DbSet<TipoCorso> TipoCorsi { get; set; }



    }
}
