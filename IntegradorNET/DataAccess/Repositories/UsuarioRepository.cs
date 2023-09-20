using System;
using IntegradorNET.DTOs;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.Repositories.Interfaces
{
	public class UsuarioRepository : Repository<Entities.Usuario>, IUsuarioRepository
	{
		public UsuarioRepository(ApplicationDbContext context) : base(context)
		{

		}

		public async Task<Usuario?> AuthenticateCredentials(AuthenticateDto dto)
		{
			var contrasenaEncriptada = PasswordEncryptHelper.EncryptPassword(dto.Contrasena, dto.Email);

            return await _context.Usuarios.SingleOrDefaultAsync(x => x.Email == dto.Email && x.Contrasena == contrasenaEncriptada);
		}
	}
}

