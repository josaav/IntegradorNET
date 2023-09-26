using System;
namespace IntegradorNET.DTOs
{
	public class TrabajoDetalleDto
	{
        public int CodTrabajo { get; set; }
        public DateTime Fecha { get; set; }
        public int CodProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public int CodServicio { get; set; }
        public string DescripcionServicio { get; set; }
        public int CantHoras { get; set; }
        public double ValorHora { get; set; }
        public double Costo { get; set; }
        public int Eliminado { get; set; }
    }
}

