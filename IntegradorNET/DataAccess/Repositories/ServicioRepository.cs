using System;
using IntegradorNET.DataAccess.Repositories.Interfaces;
using IntegradorNET.DTOs;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.Repositories
{
	public class ServicioRepository : Repository<Entities.Servicio>, IServicioRepository
    {
		public ServicioRepository(ApplicationDbContext context) : base(context)
        {
		}

        public async Task<List<Servicio>> ObtenerServiciosActivos()
        {
            var lista = await _context.Set<Servicio>()
                .Where(proyecto => (int)proyecto.Estado == 1)
                .ToListAsync();
            return lista;
        }

        public override async Task<bool> Actualizar(Servicio servicioActualizado)
        {
            var servicio = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == servicioActualizado.Id);
            if (servicio == null) { return false; }

            servicio.Descripcion = servicioActualizado.Descripcion;
            servicio.Estado = servicioActualizado.Estado;
            servicio.ValorHora = servicioActualizado.ValorHora;

            _context.Servicios.Update(servicio);
            return true;
        }

        public override async Task<bool> Eliminar(int id)
        {
            var servicio = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == id);
            if (servicio == null) { return false; }

            servicio.Eliminado = 1;

            _context.Servicios.Update(servicio);
            return true;
        }

        public override async Task<bool> Restaurar(int id)
        {
            var servicio = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == id);
            if (servicio == null) { return false; }

            servicio.Eliminado = 0;

            _context.Servicios.Update(servicio);
            return true;
        }
    }
}

