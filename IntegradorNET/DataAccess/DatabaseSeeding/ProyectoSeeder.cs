using System;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.DatabaseSeeding
{
    public class ProyectoSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proyecto>().HasData(
                new Proyecto
                {
                    Id = 1,
                    Nombre = "Proyecto Cuenca Andina",
                    Direccion = "Sede Lima",
                    Estado = (EstadoProyecto)2,
                    Eliminado = 0
                },
                new Proyecto
                {
                    Id = 2,
                    Nombre = "Proyecto Cuenca Noroeste Argentino",
                    Direccion = "Sede Tucumán",
                    Estado = (EstadoProyecto)3,
                    Eliminado = 0
                },
                new Proyecto
                {
                    Id = 3,
                    Nombre = "Proyecto Cuenca Marina Austral",
                    Direccion = "Sede Tucumán",
                    Estado = (EstadoProyecto)2,
                    Eliminado = 0
                },
                new Proyecto
                {
                    Id = 4,
                    Nombre = "Proyecto Cuenca Neuquina",
                    Direccion = "Sede Neuquén",
                    Estado = (EstadoProyecto)3,
                    Eliminado = 0
                },
                new Proyecto
                {
                    Id = 5,
                    Nombre = "Proyecto Gas Natural Cuenca Magdalena",
                    Direccion = "Sede Barranquilla",
                    Estado = (EstadoProyecto)1,
                    Eliminado = 0
                });

        }
    }
}

