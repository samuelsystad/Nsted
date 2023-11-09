using Microsoft.EntityFrameworkCore;
using Nsted.Models;
using System.Security.Cryptography.X509Certificates;

namespace Nsted.Data
{
    public class NstedDbContext : DbContext
    {
        public NstedDbContext(DbContextOptions<NstedDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Kunde> Kunder { get; set; }

        public override int SaveChanges()
        {
            // You can add any custom logic here before saving changes, if needed.
            // For example, auditing, validation, or other business logic.

            return base.SaveChanges(); // Call the base class's SaveChanges method to persist changes to the database.
        }

    }
}
