using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegradorNET.Entities
{
    public enum EstadoProyecto
    {
        Pendiente = 1,
        Confirmado = 2,
        Terminado = 3
	}

	public class Proyecto
	{
        [Column("codProyecto")]
        public int Id { get; set; }

        [Column("nombre", TypeName = "VARCHAR(200)")]
        public string Nombre { get; set; }

        [Column("direccion", TypeName = "VARCHAR(200)")]
        public string Direccion { get; set; }

        [Column("estado")]
        public EstadoProyecto Estado { get; set; }

        [Column("eliminado")]
        public int Eliminado { get; set; } = 0;
    }
}

