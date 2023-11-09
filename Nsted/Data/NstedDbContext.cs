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

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
