using System;
using IntegradorNET.DataAccess.Repositories.Interfaces;

namespace IntegradorNET.Services
{
	public interface IUnitOfWork
	{
		public UsuarioRepository UsuarioRepository { get; }
		Task<int> Complete();
	}
}

