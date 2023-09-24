using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegradorNET.DTOs;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using IntegradorNET.Infrastructure;
using IntegradorNET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegradorNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServicioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Devuelve todos los servicios con un paginado. Toma dos parámetros opcionales de la ruta,
        /// con los cuales realiza un paginado, "items" (cantidad de items por página) y "pagina"
        /// (número de página). De no ser enviados dichos parámetros, la página que muestra primero
        /// es la primera, y hay 10 ítems por página.
        /// </summary>
        /// <returns>Retorna todos los servicios</returns>

        [Authorize(Policy = "AdminConsultor")]
        [HttpGet]
        public async Task<IActionResult> ObtenerServicios()
        {
            var proyectos = await _unitOfWork.ServicioRepository.ObtenerTodos();

            List<ServicioDetalleDto> lista = proyectos.Select(entity => new ServicioDetalleDto
            {
                CodServicio = entity.Id,
                Descripcion = entity.Descripcion,
                Estado = new EstadoServicioDto
                {
                    Id = (int)entity.Estado,
                    Nombre = entity.Estado.ToString()
                },
                ValorHora = entity.ValorHora,
            }).ToList();

            int paginaPorMostrar = 1;
            int tamanoPagina = 10;

            if (Request.Query.ContainsKey("pagina")) int.TryParse(Request.Query["pagina"], out paginaPorMostrar);
            if (Request.Query.ContainsKey("items")) int.TryParse(Request.Query["items"], out tamanoPagina);

            var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();

            var listaPaginada = PaginateHelper.Paginate(lista, paginaPorMostrar, tamanoPagina, url);

            return ResponseFactory.CreateSuccessResponse(200, listaPaginada);
        }

        /// <summary>
        /// Devuelve todos los servicios con estado activo.
        /// Toma dos parámetros opcionales de la ruta, con los cuales realiza un paginado: "items" (cantidad de items
        /// por página) y "pagina" (número de página). De no ser enviados dichos parámetros, la página que muestra primero
        /// es la primera, y hay 10 ítems por página.
        /// </summary>
        /// <returns>Retorna todos los servicios activos</returns>

        [Authorize(Policy = "AdminConsultor")]
        [HttpGet("/api/Servicio/Activos")]
        public async Task<IActionResult> ObtenerServiciosActivos()
        {
            var servicios = await _unitOfWork.ServicioRepository.ObtenerServiciosActivos();

            List<ServicioDetalleDto> lista = servicios.Select(entity => new ServicioDetalleDto
            {
                CodServicio = entity.Id,
                Descripcion = entity.Descripcion,
                Estado = new EstadoServicioDto
                {
                    Id = (int)entity.Estado,
                    Nombre = entity.Estado.ToString()
                },
                ValorHora = entity.ValorHora,
            }).ToList();

            int paginaPorMostrar = 1;
            int tamanoPagina = 10;

            if (Request.Query.ContainsKey("pagina")) int.TryParse(Request.Query["pagina"], out paginaPorMostrar);
            if (Request.Query.ContainsKey("items")) int.TryParse(Request.Query["items"], out tamanoPagina);

            var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();

            var listaPaginada = PaginateHelper.Paginate(lista, paginaPorMostrar, tamanoPagina, url);

            return ResponseFactory.CreateSuccessResponse(200, listaPaginada);
        }

        /// <summary>
        /// Retorna los datos de un servicio con un Id determinado.
        /// </summary>
        /// <param name="id">Id del servicio</param>
        /// <returns>Si el servicio existe, retornará un status code 200 y sus datos, caso contrario
        /// retornará un error 500</returns>

        [Authorize(Policy = "AdminConsultor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerServicioPorId([FromRoute] int id)
        {
            var resultado = await _unitOfWork.ServicioRepository.ObtenerPorId(id);

            if (resultado == null)
            {
                return ResponseFactory.CreateErrorResponse(500, "No existe el servicio");
            }
            else
            {
                var servicio = new ServicioDetalleDto()
                {
                    CodServicio = resultado.Id,
                    Descripcion = resultado.Descripcion,
                    Estado = new EstadoServicioDto
                    {
                        Id = (int)resultado.Estado,
                        Nombre = resultado.Estado.ToString()
                    },
                    ValorHora = resultado.ValorHora,
                };

                return ResponseFactory.CreateSuccessResponse(200, servicio);
            }
        }

        /// <summary>
        /// Da de alta un nuevo servicio
        /// </summary>
        /// <param name="dto">Servicio a registrar</param>
        /// <returns>Devuelve un status code 201 si se registró con éxito</returns>

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> NuevoServicio(ServicioNuevoDto dto)
        {
            var servicio = new Servicio(dto);
            await _unitOfWork.ServicioRepository.Insert(servicio);
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(201, "Servicio creado con éxito");
        }

        /// <summary>
        /// Actualiza un servicio
        /// </summary>
        /// <param name="id">Id del servicio a actualizar</param>
        /// <param name="dto">Propiedades del servicio a actualizar</param>
        /// <returns>Devuelve un status code 200 si es que el servicio pudo ser actualizado, de lo contrario
        /// devuelve un error 500.</returns>

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarServicio([FromRoute] int id, ServicioNuevoDto dto)
        {
            var resultado = await _unitOfWork.ServicioRepository.Actualizar(new Servicio(dto, id));

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al actualizar el servicio");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Servicio actualizado con éxito");
            }
        }

        /// <summary>
        /// Elimina un servicio (borrado lógico), actualizando el campo "Eliminado" y haciéndolo equivalente a 1
        /// </summary>
        /// <param name="id">Id del servicio a eliminar</param>
        /// <returns>Devuelve un status code 200 en el caso de que pueda eliminarse, de lo contrario
        /// devuelve un error 500</returns>

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarServicio([FromRoute] int id)
        {
            var resultado = await _unitOfWork.ServicioRepository.Eliminar(id);

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al eliminar el servicio");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Servicio eliminado con éxito");
            }
        }

        /// <summary>
        /// Restaura un servicio que ha sido borrado lógicamente, actualizando el campo "Eliminado" y volviéndolo
        /// equivalente a 0
        /// </summary>
        /// <param name="id">Id del servicio a restaurar</param>
        /// <returns>Retorna un status code 200 si es que el servicio pudo ser restaurado exitosamente, de lo contrario
        /// retorna un error 500</returns>

        [Authorize(Policy = "Admin")]
        [HttpPut("/api/Servicio/Restaurar/{id}")]
        public async Task<IActionResult> RestaurarServicio([FromRoute] int id)
        {
            var resultado = await _unitOfWork.ServicioRepository.Restaurar(id);

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al restaurar el servicio");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Servicio restaurado con éxito");
            }
        }

    }
}

