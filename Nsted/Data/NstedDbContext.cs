using Microsoft.EntityFrameworkCore;
using Nsted.Models;

namespace Nsted.Data
{
    // Databasekontekst for Nsted-applikasjonen, håndterer interaksjonen med databasen.
    public class NstedDbContext : DbContext
    {
        // Konstruktør som konfigurerer konteksten med gitte alternativer.
        public NstedDbContext(DbContextOptions<NstedDbContext> options) : base(options)
        {
        }

        // DbSet som representerer kunder i databasen.
        public DbSet<Kunde> Kunder { get; set; }

        // DbSet som representerer registreringer i databasen.
        public DbSet<Registrering> Registreringer { get; set; }

        // DbSet som representerer brukere i databasen.
        public DbSet<User> Users { get; set; }

        // DbSet som representerer serviceskjemaer i databasen.
        public DbSet<ServiceSkjema> ServiceSkjemas { get; set; }

        // DbSet som representerer fullførte ordrer i databasen.
        public DbSet<FullførtOrdre> FullførteOrdrer { get; set; }

        // Lagrer endringer i databasen.
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
