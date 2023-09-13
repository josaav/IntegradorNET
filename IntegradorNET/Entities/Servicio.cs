using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegradorNET.Entities
{
    public enum EstadoServicio
    {
        Inactivo = 0,
        Activo = 1,
    }

    public class Servicio
	{
        [Column("codServicio")]
        public int Id { get; set; }

        [Column("descr", TypeName = "VARCHAR(200)")]
        public string Descripcion { get; set; }

        [Column("estado")]
        public EstadoServicio Estado { get; set; }

        [Column("valorHora", TypeName = "DECIMAL(19,4)")]
        public int ValorHora { get; set; }

        [Column("eliminado")]
        public int Eliminado { get; set; } = 0;
    }
}

