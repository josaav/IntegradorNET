using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegradorNET.Entities
{
    public class Trabajo
    {
        [Column("codTrabajo")]
        public int Id { get; set; }

        [Column("fecha", TypeName = "DATETIME")]
        public DateTime Fecha { get; set; }

		[ForeignKey("codProyecto")]
		public Proyecto Proyecto { get; set; }

        [ForeignKey("codServicio")]
        public Servicio Servicio { get; set; }

        [Column("cantHoras")]
        public int CantHoras { get; set; }

        [Column("valorHora", TypeName = "DECIMAL(19,4)")]
        public int ValorHora { get; set; }

        [Column("costo", TypeName = "DECIMAL(19,4)")]
        public int Costo { get; set; }

        [Column("eliminado")]
        public int Eliminado { get; set; } = 0;
    }
}

