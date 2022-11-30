using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiLibrary.Entidades;

namespace WebApiLibrary
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Prestamo>()
        //        .HasKey(al => new { al., al.ParticipanteId });
        //}

        //Usuarios y Carreras
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carrera> Carreras { get; set; }

        //Libros
        public DbSet<TituloLibro> TituloLibros { get; set; }
        public DbSet<Libro> Libros { get; set; }

        //Prestamos y Deudas
        public DbSet<Deuda> Deudas { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
    }
}
