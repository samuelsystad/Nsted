using Microsoft.EntityFrameworkCore;
using Nsted.Models; // Assuming User is within this namespace


namespace Nsted.Data
{
    public class NstedDbContext : DbContext
    {
        public NstedDbContext(DbContextOptions<NstedDbContext> options) : base(options)
        {

        }

        // Existing DbSets
        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Registrering> Registreringer { get; set; }


        // Add DbSet for User
        public DbSet<User> Users { get; set; }

        public DbSet<ServiceSkjema> ServiceSkjemas { get; set; }

        public DbSet<FullførtOrdre> FullførteOrdrer { get; set; }


        public override int SaveChanges()
        {
            // Custom logic before saving changes...


            return base.SaveChanges(); // Persist changes to the database.
        }

        // Other DbContext configuration...
    }
}
