using System;
namespace IntegradorNET.DTOs
{
	public class ProyectoDetalleDto
	{
		public int CodProyecto { get; set; }
		public string Nombre { get; set; }
		public string Direccion { get; set; }
		public EstadoProyectoDto Estado { get; set; }
		public int Eliminado { get; set; }
	}
}

