﻿using System;
using IntegradorNET.DataAccess.Repositories;
using IntegradorNET.DataAccess.Repositories.Interfaces;

namespace IntegradorNET.Services
{
	public interface IUnitOfWork
	{
		public UsuarioRepository UsuarioRepository { get; }
        public ProyectoRepository ProyectoRepository { get; }
        public ServicioRepository ServicioRepository { get; }
        public TrabajoRepository TrabajoRepository { get; }
        Task<int> Complete();
	}
}

