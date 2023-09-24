using System;
using IntegradorNET.DataAccess.Repositories.Interfaces;
using IntegradorNET.DTOs;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.Repositories
{
	public class UsuarioRepository : Repository<Entities.Usuario>, IUsuarioRepository
	{
		public UsuarioRepository(ApplicationDbContext context) : base(context)
		{

		}

		public async Task<Usuario?> AuthenticateCredentials(AuthenticateDto dto)
		{
			var contrasenaEncriptada = PasswordEncryptHelper.EncryptPassword(dto.Contrasena, dto.Email);

            return await _context.Usuarios.SingleOrDefaultAsync(x => x.Email == dto.Email && x.Contrasena == contrasenaEncriptada && x.Eliminado == 0);
		}

        public override async Task<bool> Actualizar(Usuario usuarioActualizado)
		{
			var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioActualizado.Id);
			if (usuario == null) { return false; }

			usuario.Nombre = usuarioActualizado.Nombre;
			usuario.Dni = usuarioActualizado.Dni;
			usuario.Email = usuarioActualizado.Email;
			usuario.Tipo = usuarioActualizado.Tipo;
			usuario.Contrasena = usuarioActualizado.Contrasena;

			_context.Usuarios.Update(usuario);
			return true;
		}

        public override async Task<bool> Eliminar(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null) { return false; }

			usuario.Eliminado = 1;

            _context.Usuarios.Update(usuario);
            return true;
        }

        public override async Task<bool> Restaurar(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null) { return false; }

            usuario.Eliminado = 0;

            _context.Usuarios.Update(usuario);
            return true;
        }

        public async Task<bool> UsuarioExiste(string email)
        {
            return await _context.Usuarios.AnyAsync(x => x.Email == email);
        }

    }
}

