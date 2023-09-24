using System;
using IntegradorNET.DataAccess.Repositories;
using IntegradorNET.DataAccess.Repositories.Interfaces;

namespace IntegradorNET.Services
{
	public interface IUnitOfWork
	{
		public UsuarioRepository UsuarioRepository { get; }
        public ProyectoRepository ProyectoRepository { get; }
        Task<int> Complete();
	}
}

