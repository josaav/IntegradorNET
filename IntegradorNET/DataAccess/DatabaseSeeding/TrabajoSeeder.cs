using System;
using IntegradorNET.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.DatabaseSeeding
{
	public class TrabajoSeeder : IEntitySeeder
	{
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trabajo>().HasData(
                new Trabajo
                {
                    Id = 1,
                    Fecha = DateTime.Now,
                    ProyectoId = 1,
                    ServicioId = 2,
                    CantHoras = 35,
                    ValorHora = 300,
                    Costo = 10500,
                    Eliminado = 0,
                },
                new Trabajo
                {
                    Id = 2,
                    Fecha = DateTime.Now,
                    ProyectoId = 4,
                    ServicioId = 5,
                    CantHoras = 48,
                    ValorHora = 400,
                    Costo = 19200,
                    Eliminado = 0,
                },
                new Trabajo
                {
                    Id = 3,
                    Fecha = DateTime.Now,
                    ProyectoId = 4,
                    ServicioId = 2,
                    CantHoras = 35,
                    ValorHora = 320,
                    Costo = 11200,
                    Eliminado = 0,
                },
                new Trabajo
                {
                    Id = 4,
                    Fecha = DateTime.Now,
                    ProyectoId = 2,
                    ServicioId = 3,
                    CantHoras = 60,
                    ValorHora = 200,
                    Costo = 12000,
                    Eliminado = 0,
                },
                new Trabajo
                {
                    Id = 5,
                    Fecha = DateTime.Now,
                    ProyectoId = 1,
                    ServicioId = 1,
                    CantHoras = 35,
                    ValorHora = 450,
                    Costo = 15750,
                    Eliminado = 0,
                },
                new Trabajo
                {
                    Id = 6,
                    Fecha = DateTime.Now,
                    ProyectoId = 3,
                    ServicioId = 1,
                    CantHoras = 50,
                    ValorHora = 300,
                    Costo = 15000,
                    Eliminado = 0,
                },
                new Trabajo
                {
                    Id = 7,
                    Fecha = DateTime.Now,
                    ProyectoId = 5,
                    ServicioId = 4,
                    CantHoras = 45,
                    ValorHora = 400,
                    Costo = 18500,
                    Eliminado = 0,
                }
                );
        }
    }
}

