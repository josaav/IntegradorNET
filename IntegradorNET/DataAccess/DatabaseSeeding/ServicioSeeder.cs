using System;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.DatabaseSeeding
{
	public class ServicioSeeder : IEntitySeeder
	{
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servicio>().HasData(
                new Servicio
                {
                    Id = 1,
                    Descripcion = "Exploración Geofísica",
                    Estado = (EstadoServicio)1,
                    Eliminado = 0
                },
                new Servicio
                {
                    Id = 2,
                    Descripcion = "Peforación Direccional y Horizontal",
                    Estado = (EstadoServicio)1,
                    Eliminado = 0
                },
                new Servicio
                {
                    Id = 3,
                    Descripcion = "Ingeniería de Reservorios",
                    Estado = (EstadoServicio)0,
                    Eliminado = 0
                },
                new Servicio
                {
                    Id = 4,
                    Descripcion = "Transporte y Logística",
                    Estado = (EstadoServicio)1,
                    Eliminado = 0
                },
                new Servicio
                {
                    Id = 5,
                    Descripcion = "Mantenimiento de Plataformas Offshore",
                    Estado = (EstadoServicio)1,
                    Eliminado = 0
                });
        }
    }
}

