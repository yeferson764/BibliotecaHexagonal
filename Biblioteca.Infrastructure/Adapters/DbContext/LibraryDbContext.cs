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
            // Configurar entidades sin relaciones de navegación
            // Solo configuraciones básicas de las propiedades

            // Configuración de Persona
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

                // Índice único para cédula
                entity.HasIndex(e => e.Cedula)
                    .IsUnique();
            });

            // Configuración de Material
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

                // Índice único para título
                entity.HasIndex(e => e.Titulo)
                    .IsUnique();
            });

            // Configuración de Prestamo
            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PersonaId)
                    .IsRequired();
                entity.Property(e => e.MaterialId)
                    .IsRequired();
                entity.Property(e => e.FechaDevolucion)
                    .IsRequired(false); // Nullable

                // La propiedad Devuelto es computed, no la mapeamos
                entity.Ignore(e => e.Devuelto);
            });

            // Configuración de Rol
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RolName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.CapacidadPrestamo)
                    .IsRequired();
            });

            // Configuración de TipoMaterial
            modelBuilder.Entity<TipoMaterial>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // NO crear foreign keys - solo configurar las propiedades como int
            // Esto mantiene la arquitectura hexagonal sin relaciones de EF
        }
    }
}