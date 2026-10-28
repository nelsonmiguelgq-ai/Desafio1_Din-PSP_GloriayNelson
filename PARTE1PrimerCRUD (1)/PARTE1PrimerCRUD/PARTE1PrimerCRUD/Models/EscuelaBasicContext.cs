using Microsoft.EntityFrameworkCore;

namespace PARTE1PrimerCRUD.Models;

public partial class EscuelaBasicContext : DbContext
{
    // Constructor que recibe la configuración (la que prepara App)
    public EscuelaBasicContext(DbContextOptions<EscuelaBasicContext> options)
        : base(options)
    {
    }

    // La tabla "estudiantes" vista desde C#
    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    // Cómo se corresponden las clases con las tablas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });
    }
}