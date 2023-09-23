using System;
namespace IntegradorNET.DataAccess.Repositories.Interfaces
{
	public interface IRepository<T> where T : class
	{
		public Task<List<T>> ObtenerTodos();
		public Task<bool> Actualizar(T entity);
		public Task<bool> Eliminar(int id);
	}
}

