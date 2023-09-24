using System;
namespace IntegradorNET.DTOs
{
	public class PaginateDataDto<T>
	{
		public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
        public int PaginasTotales { get; set; }
        public int ItemsTotales { get; set; }
        public string UrlPrev { get; set; }
        public string UrlSig { get; set; }
        public List<T> Items { get; set; }
    }
}

