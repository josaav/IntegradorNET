using System;
using IntegradorNET.DTOs;

namespace IntegradorNET.Helpers
{
	public static class PaginateHelper
	{
		public static PaginateDataDto<T> Paginate<T>(List<T> itemsAPaginar, int paginaActual, int tamanoPagina, string url)
		{
			var itemsTotales = itemsAPaginar.Count;
			var paginasTotales = (int)Math.Ceiling((double)itemsTotales / tamanoPagina);

			var itemsPaginados = itemsAPaginar.Skip((paginaActual - 1) * tamanoPagina).Take(tamanoPagina).ToList();

			var urlPrev = paginaActual > 1 ? $"{url}?pagina={paginaActual - 1}" : null;
			var urlSig = paginaActual < paginasTotales ? $"{url}?pagina={paginaActual + 1}" : null;

			return new PaginateDataDto<T>()
			{
                PaginaActual = paginaActual,
                TamanoPagina = tamanoPagina,
                ItemsTotales = itemsTotales,
				PaginasTotales = paginasTotales,
				UrlPrev = urlPrev,
				UrlSig = urlSig,
				Items = itemsPaginados
			};

        }
	
	}
}

