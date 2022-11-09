using IdentityApp.Models;

namespace IdentityApp.Data
{
    public class IdentityAppContext : DbContext
    {
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options)
            : base(options)
        {
        }

        public DbSet<Invoice> Invoice { get; set; } = default!;
    }
}
