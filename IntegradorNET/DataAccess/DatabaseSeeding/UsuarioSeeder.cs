using System;
using System.Data;
using IntegradorNET.DataAccess.DatabaseSeeding;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.DatabaseSeeding
{
	public class UsuarioSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Juan Pérez",
                    Dni = 26175345,
                    Email = "jperez@techoil.com",
                    Tipo = (TipoUsuario)1,
                    Contrasena = PasswordEncryptHelper.EncryptPassword("1234", "jperez@techoil.com"),
                    Eliminado = 0
                },
                new Usuario
                {
                    Id = 2,
                    Nombre = "José Gómez",
                    Dni = 36565454,
                    Email = "jgomez@techoil.com",
                    Tipo = (TipoUsuario)2,
                    Contrasena = PasswordEncryptHelper.EncryptPassword("12345678", "jgomez@techoil.com"),
                    Eliminado = 0
                },
                new Usuario
                {
                    Id = 3,
                    Nombre = "María Álvarez",
                    Dni = 32115082,
                    Email = "malvarez@techoil.com",
                    Tipo = (TipoUsuario)2,
                    Contrasena = PasswordEncryptHelper.EncryptPassword("abcdefg", "malvarez@techoil.com"),
                    Eliminado = 0
                });
        }
    }
}

