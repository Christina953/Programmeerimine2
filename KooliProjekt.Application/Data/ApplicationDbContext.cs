using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        //kõik decimal väljad saavad vaikimisi 2 komakohta
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
           configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }
        // autode hinnad vajavad rohkem komakohti.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().Property(c => c.PricePerMinute).HasPrecision(18, 4);
            modelBuilder.Entity<Car>().Property(c => c.PricePerKilometer).HasPrecision(18, 4);
        }
    }
}
