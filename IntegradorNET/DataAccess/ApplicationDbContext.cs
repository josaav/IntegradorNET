using System;
using IntegradorNET.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

		public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Trabajo> Trabajos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }
}

