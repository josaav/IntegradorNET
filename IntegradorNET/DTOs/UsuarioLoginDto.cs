using System;
namespace IntegradorNET.DTOs
{
    public class UsuarioLoginDto
	{
		public string Nombre { get; set; }
		public int Dni { get; set; }
        public string Email { get; set; }
        public TipoUsuarioDto Tipo { get; set; }
		public string Token { get; set; }
	}
}

