using System;
namespace IntegradorNET.DTOs
{
	public class ServicioDetalleDto
	{
        public int CodServicio { get; set; }
        public string Descripcion { get; set; }
        public EstadoServicioDto Estado { get; set; }
        public int ValorHora { get; set; }
    }
}

