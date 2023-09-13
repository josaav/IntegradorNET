using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegradorNET.Entities
{
	public class Usuario
	{
        public enum TipoUsuario
        {
            Administrador = 1,
            Consultor = 2
        }

        [Column("codUsuario")]
        public int Id { get; set; }

        [Column("nombre", TypeName = "VARCHAR(50)")]
        public string Nombre { get; set; }

        [Column("dni")]
        public int Dni { get; set; }

        [Column("tipo")]
        public TipoUsuario Tipo { get; set; }

        [Column("contrasena", TypeName = "VARCHAR(50)")]
        public string Contrasena { get; set; }

        [Column("eliminado")]
        public int Eliminado { get; set; } = 0;

    }
}

