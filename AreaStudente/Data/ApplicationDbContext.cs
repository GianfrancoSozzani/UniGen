using AreaStudente.Models;
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
        public DbSet<Comunicazione> Comunicazioni { get; set; } // Example DbSet for the Comunicazione entity
        public DbSet<Docente> Docenti { get; set; }
        public DbSet<Corso> Corsi { get; set; }
        public DbSet<Facolta> Facolta { get; set; }
        public DbSet<TipiCorsi> TipiCorsi { get; set; }
        public DbSet<Esame> Esami { get; set; }
        //public DbSet<Operatore> Operatori { get; set; }

        public DbSet<PianoStudioPersonale> PianiStudioPersonali { get; set; }

      
        public DbSet<Pagamento> Pagamenti { get; set; }




    }
}
