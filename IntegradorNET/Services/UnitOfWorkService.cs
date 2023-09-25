using System;
using IntegradorNET.DataAccess;
using IntegradorNET.DataAccess.Repositories;
using IntegradorNET.DataAccess.Repositories.Interfaces;

namespace IntegradorNET.Services
{
	public class UnitOfWorkService : IUnitOfWork	
	{
        private readonly ApplicationDbContext _context;
        public UsuarioRepository UsuarioRepository { get; private set; }
        public ProyectoRepository ProyectoRepository { get; private set; }
        public ServicioRepository ServicioRepository { get; private set; }
        public TrabajoRepository TrabajoRepository { get; private set; }

        public UnitOfWorkService(ApplicationDbContext context)
        {
            _context = context;
            UsuarioRepository = new UsuarioRepository(_context);
            ProyectoRepository = new ProyectoRepository(_context);
            ServicioRepository = new ServicioRepository(_context);
            TrabajoRepository = new TrabajoRepository(_context);
        }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }
    }
}

