using System;
namespace IntegradorNET.DTOs
{
	public class UsuarioDetalleDto
	{
        public int codUsuario { get; set; }
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Email { get; set; }
        public TipoUsuarioDto Tipo { get; set; }
        public int Eliminado { get; set; }
    }
}

