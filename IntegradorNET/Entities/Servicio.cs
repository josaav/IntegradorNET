using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IntegradorNET.DTOs;

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

        [Column("eliminado")]
        public int Eliminado { get; set; } = 0;

        [Column("valorHora", TypeName = "DECIMAL(19,2)")]
        public int ValorHora { get; set; } = 0;

        public Servicio(ServicioNuevoDto dto)
        {
            Descripcion = dto.Descripcion;
            Estado = (EstadoServicio)dto.Estado;
            ValorHora = dto.ValorHora;
        }

        public Servicio(ServicioNuevoDto dto, int id)
        {
            Id = id;
            Descripcion = dto.Descripcion;
            Estado = (EstadoServicio)dto.Estado;
            ValorHora = dto.ValorHora;
        }

        public Servicio()
        {
        }
    }
}

