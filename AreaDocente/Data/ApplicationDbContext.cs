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
    }
}
