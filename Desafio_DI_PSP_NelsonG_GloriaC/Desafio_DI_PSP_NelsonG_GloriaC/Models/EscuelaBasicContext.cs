using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Desafio_DI_PSP_NelsonG_GloriaC.Models;

public partial class EscuelaBasicContext : DbContext
{
    public EscuelaBasicContext()
    {
    }

    public EscuelaBasicContext(DbContextOptions<EscuelaBasicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Presupuesto> Presupuestos { get; set; }

    public virtual DbSet<PresupuestoDetalle> PresupuestoDetalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<TiposTarea> TiposTareas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Cliente).HasConstraintName("fk_cliente_usuario");
        });

        modelBuilder.Entity<Presupuesto>(entity =>
        {
            entity.HasKey(e => e.IdPresupuesto).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'Pendiente'");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Presupuestos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_presupuesto_cliente");
        });

        modelBuilder.Entity<PresupuestoDetalle>(entity =>
        {
            entity.HasKey(e => new { e.IdPresupuesto, e.IdProducto })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.Cantidad).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.IdPresupuestoNavigation).WithMany(p => p.PresupuestoDetalles).HasConstraintName("fk_detalle_presupuesto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.PresupuestoDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalle_producto");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.Property(e => e.TipoMaterial).HasDefaultValueSql("'Pendiente'");
        });

        modelBuilder.Entity<TiposTarea>(entity =>
        {
            entity.HasKey(e => e.IdTipTarea).HasName("PRIMARY");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}