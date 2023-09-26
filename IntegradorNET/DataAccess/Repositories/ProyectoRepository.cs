using System;
using IntegradorNET.DataAccess.Repositories.Interfaces;
using IntegradorNET.DTOs;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.Repositories
{
	public class ProyectoRepository : Repository<Entities.Proyecto>, IProyectoRepository
	{
		public ProyectoRepository(ApplicationDbContext context) : base(context)
		{

		}

        public async Task<List<Proyecto>> ObtenerProyectosPorEstado(int id)
        {
            var lista = await _context.Set<Proyecto>()
                .Where(proyecto => (int)proyecto.Estado == id)
                .ToListAsync();
            return lista;
        }

        public override async Task<bool> Actualizar(Proyecto proyectoActualizado)
        {
            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(x => x.Id == proyectoActualizado.Id);
            if (proyecto == null) { return false; }

            proyecto.Nombre = proyectoActualizado.Nombre;
            proyecto.Direccion = proyectoActualizado.Direccion;
            proyecto.Estado = proyectoActualizado.Estado;

            _context.Proyectos.Update(proyecto);
            return true;
        }

        public override async Task<bool> Eliminar(int id)
        {
            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(x => x.Id == id);
            if (proyecto == null) { return false; }

            proyecto.Eliminado = 1;

            _context.Proyectos.Update(proyecto);
            return true;
        }

        public override async Task<bool> Restaurar(int id)
        {
            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(x => x.Id == id);
            if (proyecto == null) { return false; }

            proyecto.Eliminado = 0;

            _context.Proyectos.Update(proyecto);
            return true;
        }

    }
}

