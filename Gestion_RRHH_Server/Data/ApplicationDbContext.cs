using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using Gestion_RRHH_Server.Models;

namespace Gestion_RRHH_Server.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


        public DbSet<Ejemplo> Ejemplos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Direccion> Direccions { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Seccion> Seccions { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Motivo> Motivos { get; set; }


    }
}

