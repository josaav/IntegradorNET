using System;
using IntegradorNET.Entities;

namespace IntegradorNET.DTOs
{
	public class UsuarioRegistroDto
	{
		public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }
        public string Contrasena { get; set; }
    }
}

