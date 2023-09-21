using System;
namespace IntegradorNET.DTOs
{
    public class TipoUsuarioLoginDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class UsuarioLoginDto
	{
		public string Nombre { get; set; }
		public int Dni { get; set; }
        public string Email { get; set; }
        public TipoUsuarioLoginDto Tipo { get; set; }
		public string Token { get; set; }
	}
}

