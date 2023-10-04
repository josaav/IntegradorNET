using System;
using IntegradorNET.DataAccess.Repositories.Interfaces;
using IntegradorNET.DTOs;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.Repositories
{
	public class TrabajoRepository : Repository<Entities.Trabajo>, ITrabajoRepository
	{
		public TrabajoRepository(ApplicationDbContext context) : base(context)
		{
		}

        public override async Task<List<Trabajo>> ObtenerTodos()
        {
            var lista = await _context.Set<Trabajo>()
                .Include(b => b.Proyecto)
                .Include(b => b.Servicio)
                .ToListAsync();
            return lista;
        }

        public override async Task<Trabajo> ObtenerPorId(int id)
        {
            return await _context.Set<Trabajo>()
                        .Include(b => b.Proyecto)
                        .Include(b => b.Servicio)
                        .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Trabajo> NuevoTrabajo(TrabajoNuevoDto dto)
        {
            var servicio = await _context.Set<Servicio>()
                                  .FirstOrDefaultAsync(t => t.Id == dto.CodServicio);

            var valorHora = servicio.ValorHora;

            var trabajo = new Trabajo()
            {
                Fecha = dto.Fecha,
                ProyectoId = dto.CodProyecto,
                ServicioId = dto.CodServicio,
                CantHoras = dto.CantHoras,
                ValorHora = valorHora,
                Costo = dto.CantHoras * valorHora,
            };

            return trabajo;
        }

        public async Task<bool> Actualizar(TrabajoNuevoDto trabajoActualizado, int id)
        {
            var trabajo = await _context.Trabajos.FirstOrDefaultAsync(x => x.Id == id);
            if (trabajo == null) { return false; }

            var servicio = await _context.Set<Servicio>()
                                  .FirstOrDefaultAsync(t => t.Id == trabajoActualizado.CodServicio);

            var valorHora = servicio.ValorHora;

            trabajo.Fecha = trabajoActualizado.Fecha;
            trabajo.ProyectoId = trabajoActualizado.CodProyecto;
            trabajo.ServicioId = trabajoActualizado.CodServicio;
            trabajo.CantHoras = trabajoActualizado.CantHoras;
            trabajo.ValorHora = valorHora;
            trabajo.Costo = trabajoActualizado.CantHoras * valorHora;

            _context.Trabajos.Update(trabajo);
            return true;
        }

        public override async Task<bool> Eliminar(int id)
        {
            var trabajo = await _context.Trabajos.FirstOrDefaultAsync(x => x.Id == id);
            if (trabajo == null) { return false; }

            trabajo.Eliminado = 1;

            _context.Trabajos.Update(trabajo);
            return true;
        }

        public override async Task<bool> Restaurar(int id)
        {
            var trabajo = await _context.Trabajos.FirstOrDefaultAsync(x => x.Id == id);
            if (trabajo == null) { return false; }

            trabajo.Eliminado = 0;

            _context.Trabajos.Update(trabajo);
            return true;
        }


    }
}

