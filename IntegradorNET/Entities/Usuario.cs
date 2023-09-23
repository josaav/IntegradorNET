using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IntegradorNET.DTOs;
using IntegradorNET.Helpers;

namespace IntegradorNET.Entities
{
    public enum TipoUsuario
    {
        Administrador = 1,
        Consultor = 2
    }

    public class Usuario
	{
        [Column("codUsuario")]
        public int Id { get; set; }

        [Column("nombre", TypeName = "VARCHAR(50)")]
        public string Nombre { get; set; }

        [Column("dni")]
        public int Dni { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("tipo")]
        public TipoUsuario Tipo { get; set; }

        [Column("contrasena", TypeName = "VARCHAR(100)")]
        public string Contrasena { get; set; }

        [Column("eliminado")]
        public int Eliminado { get; set; } = 0;

        public Usuario(UsuarioRegistroDto dto)
        {
            Nombre = dto.Nombre;
            Dni = dto.Dni;
            Email = dto.Email;
            Tipo = (TipoUsuario)dto.Tipo;
            Contrasena = PasswordEncryptHelper.EncryptPassword(dto.Contrasena, dto.Email);
        }

        public Usuario(UsuarioRegistroDto dto, int id)
        {
            Id = id;
            Nombre = dto.Nombre;
            Dni = dto.Dni;
            Email = dto.Email;
            Tipo = (TipoUsuario)dto.Tipo;
            Contrasena = PasswordEncryptHelper.EncryptPassword(dto.Contrasena, dto.Email);
        }

        public Usuario()
        {

        }

    }
}

