using Microsoft.EntityFrameworkCore;
using testTEA.Models;

namespace testTEA.Models
{
    public class testContext : DbContext
    {
        public testContext(DbContextOptions<testContext> options) : base(options)
        {
        }

        public DbSet<Usuario> usuarios { get; set; } 
        public DbSet<tests> tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para que EF Core entienda que id_usuario se genera automáticamente
            modelBuilder.Entity<Usuario>()
                .Property(u => u.id_usuario)
                .ValueGeneratedOnAdd();
        }
        public DbSet<testTEA.Models.preguntas> preguntas { get; set; } = default!;
    }
}
