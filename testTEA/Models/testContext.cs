using Microsoft.EntityFrameworkCore;

namespace testTEA.Models
{
    public class testContext : DbContext
    {
        public testContext(DbContextOptions<testContext> options) : base(options)
        {
        }
        public DbSet<Usuario> usuarios { get; set; }

    }
}
