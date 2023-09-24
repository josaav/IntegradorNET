using System;
using IntegradorNET.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntegradorNET.DataAccess.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly ApplicationDbContext _context;

		public Repository (ApplicationDbContext context)
		{
			_context = context;
		}

        public virtual async Task<List<T>> ObtenerTodos()
        {
			// Retornará los valores de las entidades siempre que la propiedad "Eliminado" sea igual a 0 (borrado lógico)
            var lista = await _context.Set<T>().ToListAsync();
            return lista.Where(entity => (int)entity.GetType().GetProperty("Eliminado").GetValue(entity) == 0).ToList();
        }

        public virtual async Task<T?> ObtenerPorId(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

		public virtual async Task<bool> Insert (T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return true;
		}

        public virtual Task<bool> Actualizar(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Restaurar(int id)
        {
            throw new NotImplementedException();
        }
    }
}

