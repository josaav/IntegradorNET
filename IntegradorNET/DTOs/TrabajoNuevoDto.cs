using System;
namespace IntegradorNET.DTOs
{
	public class TrabajoNuevoDto
	{
		public DateTime Fecha { get; set; }
		public int CodProyecto { get; set; }
		public int CodServicio { get; set; }
		public int CantHoras { get; set; }
	}
}

