using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IntegradorNET.DTOs;

namespace IntegradorNET.Entities
{
    public class Trabajo
    {
        [Column("codTrabajo")]
        public int Id { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("codProyecto")]
        public int ProyectoId { get; set; }

        [ForeignKey("ProyectoId")]
        public Proyecto? Proyecto { get; set; }

        [Column("codServicio")]
        public int ServicioId { get; set; }

        [ForeignKey("ServicioId")]
        public Servicio? Servicio { get; set; }

        [Column("cantHoras")]
        public int CantHoras { get; set; }

        [Column("valorHora", TypeName = "DECIMAL(19,2)")]
        public double ValorHora { get; set; }

        [Column("costo", TypeName = "DECIMAL(19,2)")]
        public double Costo { get; set; }

        [Column("eliminado")]
        public int Eliminado { get; set; } = 0;

        public Trabajo(TrabajoNuevoDto dto, int id)
        {
            Id = id;
            Fecha = dto.Fecha;
            ProyectoId = dto.CodProyecto;
            ServicioId = dto.CodServicio;
            CantHoras = dto.CantHoras;
        }

        public Trabajo()
        {
        }

    }
}

