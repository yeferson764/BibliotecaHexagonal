using Microsoft.EntityFrameworkCore;
using Biblioteca.Domain.Models;

namespace Biblioteca.Infrastructure.Persistence
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<TipoMaterial> TiposMaterial { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.RolId)
                    .IsRequired();

                entity.HasIndex(e => e.Cedula)
                    .IsUnique();
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.TipoMaterialId)
                    .IsRequired();
                entity.Property(e => e.CantidadRegistrada)
                    .IsRequired();
                entity.Property(e => e.CantidadActual)
                    .IsRequired();

                entity.HasIndex(e => e.Titulo)
                    .IsUnique();
            });

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PersonaId)
                    .IsRequired();
                entity.Property(e => e.MaterialId)
                    .IsRequired();
                entity.Property(e => e.FechaDevolucion)
                    .IsRequired(false);

                entity.Ignore(e => e.Devuelto);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RolName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.CapacidadPrestamo)
                    .IsRequired();
            });

            modelBuilder.Entity<TipoMaterial>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}